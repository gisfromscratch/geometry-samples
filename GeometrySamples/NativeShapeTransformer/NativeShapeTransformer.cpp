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
 * @param indexX the index of the x coordinate.
 * @param indexY the index of the y coordinate.
 */
static void convertCsvFileToWkb(char *file, char *sep, int indexX, int indexY)
{
	std::ifstream inputStream(file);
	if (!inputStream)
	{
		std::cerr << file << " is not accessible!" << std::endl;
		return;
	}

	std::string outFile(file);
	outFile += ".wkb";
	std::ofstream outputStream(outFile, std::ios::binary);
	if (!outputStream)
	{
		std::cerr << outFile << " is not writable!" << std::endl;
		return;
	}

	const int MaxLineLength = 1024;
	char line[MaxLineLength];
	char *tokenInput, *token, *nextToken;
	WKBPoint wkbPoint;
	wkbPoint.byteOrder = wkbByteOrder::wkbNDR;
	wkbPoint.wkbType = wkbGeometryType::wkbPoint;
	wkbPoint.point.X = 0;
	wkbPoint.point.Y = 0;
	bool hasX = false, hasY = false;
	while (inputStream.good() && outputStream.good())
	{
		hasX = false, hasY = false;
		inputStream.getline(line, MaxLineLength);
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
			outputStream.write(reinterpret_cast<char*>(&wkbPoint), sizeof(WKBPoint));
		}
	}

	outputStream.flush();
}



int main(int argc, char *args[])
{
	char *file = '\0';
	char *sep = '\0';
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
		std::cout << "Usage: -f /temp/points.csv -sep \\t -idxx 4 -idxy 3" << std::endl;
		return -1;
	}

	convertCsvFileToWkb(file, sep, indexX, indexY);

    return 0;
}

