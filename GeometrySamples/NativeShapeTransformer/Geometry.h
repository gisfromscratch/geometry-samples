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
#pragma once

typedef unsigned char byte;
typedef unsigned long uint32;

///
/// Two dimensional point.
///
struct Point {
	double X;
	double Y;
};

///
/// The geometry type.
///
enum wkbGeometryType {
	wkbPoint = 1,
	wkbLineString = 2,
	wkbPolygon = 3,
	wkbMultiPoint = 4,
	wkbMultiLineString = 5,
	wkbMultiPolygon = 6,
	wkbGeometryCollection = 7
};

///
/// The byte order.
///
enum wkbByteOrder {
	wkbXDR = 0, // Big Endian
	wkbNDR = 1	// Little Endian
};

///
/// Two dimensional point as WKB.
///
struct WKBPoint {
	byte byteOrder;
	uint32 wkbType;
	Point point;
};