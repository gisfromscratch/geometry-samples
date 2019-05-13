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

#ifndef SIMPLESPATIALREFERENCE_H
#define SIMPLESPATIALREFERENCE_H

#include <string>

enum SimpleWkid {
    Unknown = 0,
    WGS1984 = 4326
};

class SimpleSpatialReference
{
public:
    SimpleSpatialReference(SimpleWkid wkid);
    std::string toString() const;

private:
    SimpleWkid _wkid;
};

#endif // SIMPLESPATIALREFERENCE_H
