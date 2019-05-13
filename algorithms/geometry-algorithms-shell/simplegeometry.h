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

#ifndef SIMPLEGEOMETRY_H
#define SIMPLEGEOMETRY_H

#include <string>

#include "simplespatialreference.h"

class SimpleGeometry
{
public:
    virtual std::string toString() const = 0;

protected:
    SimpleGeometry(const SimpleSpatialReference &spatialReference);
    SimpleSpatialReference _spatialReference;
};

class SimplePoint2d : public SimpleGeometry
{
public:
    SimplePoint2d(const SimpleSpatialReference &spatialReference, double x, double y);
    virtual std::string toString() const;

private:
    double _x,_y;
};

#endif // SIMPLEGEOMETRY_H
