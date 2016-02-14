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
typedef long int32;

///
/// Two dimensional point.
///
struct Point {
	double X;
	double Y;
};

///
/// The WKB geometry type.
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
/// The WKB byte order.
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


///
/// The Esri geometry type.
///
enum esriGeometryType {
	esriNullShape = 0,
	esriPoint = 1,
	esriPolyline = 3,
	esriPolygon = 5,
	esriMultiPoint = 8,
	esriPointZ = 11,
	esriPolylineZ = 13,
	esriPolygonZ = 15,
	esriMultiPointZ = 18,
	esriPointM = 21,
	esriPolylineM = 23,
	esriPolygonM = 25,
	esriMultiPointM = 28,
	esriMultiPatch = 31
};

///
/// Two dimensional point as Shape.
///
struct EsriPoint {
	esriGeometryType shapeType;
	Point point;
};