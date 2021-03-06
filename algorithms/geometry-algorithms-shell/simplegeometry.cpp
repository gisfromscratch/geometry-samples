/*
 * Copyright (C) 2012 J.T.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

#include <sstream>

#include "simplegeometry.h"
#include "simplespatialreference.h"

SimpleGeometry::SimpleGeometry(const SimpleSpatialReference &spatialReference)
    : _spatialReference(spatialReference)
{
}

SimpleSpatialReference SimpleGeometry::spatialReference() const
{
    return _spatialReference;
}


SimplePoint2d::SimplePoint2d(const SimpleSpatialReference &spatialReference, double x, double y)
    : SimpleGeometry(spatialReference), _x(x), _y(y)
{
}

std::string SimplePoint2d::toString() const
{
    std::stringstream builder;
    builder << _spatialReference.toString();
    builder << std::endl << "Point:";
    builder << "{x:" << _x << ",y:" << _y << "}";
    return builder.str();
}


SimpleLineSegment2d::SimpleLineSegment2d(const SimplePoint2d &start, const SimplePoint2d &end)
    : SimpleGeometry(start.spatialReference()), _start(start), _end(end)
{
}

std::string SimpleLineSegment2d::toString() const
{
    std::stringstream builder;
    builder << _spatialReference.toString();
    builder << std::endl << "Line segment:";
    builder << "{Start:" << _start.toString() << ",End:" << _end.toString() << "}";
    return builder.str();
}
