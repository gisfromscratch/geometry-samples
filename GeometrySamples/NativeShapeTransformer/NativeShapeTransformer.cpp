/*
 * Copyright 2016 Jan Tschada
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

// NativeShapeTransformer.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

/**
 * Reads the file containing the points and converts them to a file only containing the WKB geometries.
 * @param file the path to the file.
 * @param sep the field separator.
 * @param skip the number of rows to skip.
 * @param rows the maximum number of rows to convert.
 * @param indexX the index of the x coordinate.
 * @param indexY the index of the y coordinate.
 * @return the file path to the output file.
 */
static const char* convertCsvFileToWkb(const char *file, char *sep, int skip, int rows, int indexX, int indexY)
{
	std::ifstream inputStream(file);
	if (!inputStream)
	{
		std::cerr << file << " is not accessible!" << std::endl;
		return nullptr;
	}

	// UTF-8
	/*std::locale ut8Locale(std::locale(), new std::codecvt_utf8<wchar_t>());
	inputStream.imbue(ut8Locale);*/

	// Define the output byte stream
	typedef std::basic_ofstream<byte, std::char_traits<byte> > uofstream;

	std::string outFile(file);
	std::size_t index = outFile.find_last_of('.');
	if (std::string::npos != index)
	{
		outFile.replace(outFile.begin() + index, outFile.end(), ".wkb");
	}
	else
	{
		outFile += ".wkb";
	}
	uofstream outputStream(outFile, std::ios::binary);
	if (!outputStream)
	{
		std::cerr << outFile << " is not writable!" << std::endl;
		return nullptr;
	}

	const int MaxLineLength = 1024;
	char line[MaxLineLength + sizeof(char)];
	char *tokenInput, *token, *nextToken;
	WKBPoint wkbPoint;
	wkbPoint.byteOrder = wkbByteOrder::wkbNDR;
	wkbPoint.wkbType = wkbGeometryType::wkbPoint;
	wkbPoint.point.X = 0;
	wkbPoint.point.Y = 0;
	bool hasX = false, hasY = false;
	int lineNumber = 0;
	int exportCount = 0;
	while (inputStream.good() && outputStream.good())
	{
		hasX = false, hasY = false;
		inputStream.getline(line, MaxLineLength);

		if (lineNumber < skip)
		{
			lineNumber++;
			continue;
		}

		tokenInput = line;
		for (int tokenIndex = 0; nullptr != (token = strtok_s(tokenInput, sep, &nextToken)); tokenIndex++)
		{
			if (0 == tokenIndex)
			{
				tokenInput = nullptr;
			}
			if (indexX == tokenIndex)
			{
				wkbPoint.point.X = atof(token);
				hasX = true;
			}
			else if (indexY == tokenIndex)
			{
				wkbPoint.point.Y = atof(token);
				hasY = true;
			}
		}

		if (hasX && hasY)
		{
			// Write the WKB representation
			outputStream.write(reinterpret_cast<byte*>(&wkbPoint.byteOrder), 1);
			outputStream.write(reinterpret_cast<byte*>(&wkbPoint.wkbType), 4);
			outputStream.write(reinterpret_cast<byte*>(&wkbPoint.point.X), 8);
			outputStream.write(reinterpret_cast<byte*>(&wkbPoint.point.Y), 8);

			if (0 < rows)
			{
				if (++exportCount == rows)
				{
					break;
				}
			}
		}
	}

	outputStream.flush();

	size_t pathBufferSize = outFile.length() + sizeof(char);
	char *outFilePath = new char[pathBufferSize];
	strcpy_s(outFilePath, pathBufferSize, outFile.c_str());
	return outFilePath;
}


/**
 * Reads the file containing WKB point geometries and converts them to a file containing the WKT representation.
 * @param file the path to the file.
 * @return the file path to the output file.
 */
static const char* convertWkbFileToWkt(const char *file)
{
	// Define the input byte stream
	typedef std::basic_ifstream<byte, std::char_traits<byte> > uifstream;

	uifstream inputStream(file, std::ios::binary);
	if (!inputStream)
	{
		std::cerr << file << " is not accessible!" << std::endl;
		return nullptr;
	}

	std::string outFile(file);
	std::size_t index = outFile.find_last_of('.');
	if (std::string::npos != index)
	{
		outFile.replace(outFile.begin() + index, outFile.end(), ".wkt");
	}
	else
	{
		outFile += ".wkt";
	}

	std::ofstream outputStream(outFile);
	if (!outputStream)
	{
		std::cerr << outFile << " is not writable!" << std::endl;
		return nullptr;
	}
	outputStream << std::setprecision(13);

	WKBPoint wkbPoint;
	while (inputStream.good() && outputStream.good())
	{
		// Read the WKB representation
		if (!inputStream.read(reinterpret_cast<byte*>(&wkbPoint.byteOrder), 1))
		{
			break;
		}
		inputStream.read(reinterpret_cast<byte*>(&wkbPoint.wkbType), 4);
		inputStream.read(reinterpret_cast<byte*>(&wkbPoint.point.X), 8);
		inputStream.read(reinterpret_cast<byte*>(&wkbPoint.point.Y), 8);

		// Write the WKT representation
		outputStream << "POINT (" << wkbPoint.point.X << " " << wkbPoint.point.Y << ")" << std::endl;
	}

	outputStream.flush();

	size_t pathBufferSize = outFile.length() + sizeof(char);
	char *outFilePath = new char[pathBufferSize];
	strcpy_s(outFilePath, pathBufferSize, outFile.c_str());
	return outFilePath;
}


/**
* Reads the file containing WKB point geometries and converts them to a file containing the Esri shape representation.
* @param file the path to the file.
* @return the file path to the output file.
*/
static const char* convertWkbFileToShape(const char *file)
{
	// Define the input byte stream
	typedef std::basic_ifstream<byte, std::char_traits<byte> > uifstream;

	uifstream inputStream(file, std::ios::binary);
	if (!inputStream)
	{
		std::cerr << file << " is not accessible!" << std::endl;
		return nullptr;
	}

	// Define the output byte stream
	typedef std::basic_ofstream<byte, std::char_traits<byte> > uofstream;

	std::string outFile(file);
	std::size_t index = outFile.find_last_of('.');
	if (std::string::npos != index)
	{
		outFile.replace(outFile.begin() + index, outFile.end(), ".shape");
	}
	else
	{
		outFile += ".shape";
	}

	uofstream outputStream(outFile, std::ios::binary);
	if (!outputStream)
	{
		std::cerr << outFile << " is not writable!" << std::endl;
		return nullptr;
	}

	WKBPoint wkbPoint;
	EsriPoint esriPoint;
	esriPoint.shapeType = esriGeometryType::esriPoint;
	esriPoint.point.X = 0;
	esriPoint.point.Y = 0;
	while (inputStream.good() && outputStream.good())
	{
		// Read the WKB representation
		if (!inputStream.read(reinterpret_cast<byte*>(&wkbPoint.byteOrder), 1))
		{
			break;
		}
		inputStream.read(reinterpret_cast<byte*>(&wkbPoint.wkbType), 4);
		inputStream.read(reinterpret_cast<byte*>(&wkbPoint.point.X), 8);
		inputStream.read(reinterpret_cast<byte*>(&wkbPoint.point.Y), 8);

		esriPoint.point.X = wkbPoint.point.X;
		esriPoint.point.Y = wkbPoint.point.Y;

		// Write the Esri shape representation
		outputStream.write(reinterpret_cast<byte*>(&esriPoint.shapeType), 4);
		outputStream.write(reinterpret_cast<byte*>(&esriPoint.point.X), 8);
		outputStream.write(reinterpret_cast<byte*>(&esriPoint.point.Y), 8);
	}

	outputStream.flush();

	size_t pathBufferSize = outFile.length() + sizeof(char);
	char *outFilePath = new char[pathBufferSize];
	strcpy_s(outFilePath, pathBufferSize, outFile.c_str());
	return outFilePath;
}



int main(int argc, char *args[])
{
	char *file = '\0';
	char *sep = '\0';
	int skip = 0;
	int rows = -1;
	int indexX = -1;
	int indexY = -1;
	char *lastArg = "";
	for (int index = 0; index < argc; index++)
	{
		if (0 == strcmp("-f", lastArg))
		{
			file = args[index];
		}
		else if (0 == strcmp("-sep", lastArg))
		{
			sep = args[index];
			if (0 == strcmp("\\t", sep))
			{
				sep = "\t";
			}
		}
		else if (0 == strcmp("-skip", lastArg))
		{
			skip = atoi(args[index]);
			if (skip < 0)
			{
				skip = 0;
			}
		}
		else if (0 == strcmp("-rows", lastArg))
		{
			rows = atoi(args[index]);
		}
		else if (0 == strcmp("-idxx", lastArg))
		{
			indexX = atoi(args[index]);
		}
		else if (0 == strcmp("-idxy", lastArg))
		{
			indexY = atoi(args[index]);
		}
		std::cout << args[index] << " ";
		lastArg = args[index];
	}
	std::cout << std::endl;

	if ('\0' == file || '\0' == sep || -1 == indexX || -1 == indexY)
	{
		std::cout << "Usage: -f /temp/points.csv -sep \\t -idxx 4 -idxy 3 [optional: -skip 1 -rows 100]" << std::endl;
		return -1;
	}

	std::unique_ptr<const char> wkbFile(convertCsvFileToWkb(file, sep, skip, rows, indexX, indexY));
	if (nullptr == wkbFile)
	{
		return -1;
	}

	std::unique_ptr<const char> wktFile(convertWkbFileToWkt(wkbFile.get()));
	if (nullptr == wktFile)
	{
		return -1;
	}

	std::unique_ptr<const char> shapeFile(convertWkbFileToShape(wkbFile.get()));
	if (nullptr == shapeFile)
	{
		return -1;
	}

    return 0;
}

