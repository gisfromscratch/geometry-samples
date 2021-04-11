// Licensed under the GNU Lesser General Public License Version 3.0.
// Copyright (C) 2021 Jan Tschada (gisfromscratch@live.de)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

#include "pygeom.h"

#include <pybind11/pybind11.h>
#include <pybind11/stl.h>

#include <geos/geom.h>
#include <geos/io/WKBReader.h>

using namespace geos::io;
using namespace pybind11;
using namespace std;

static bool load_features_wkt(const vector<string>& features)
{
    for (const string& wkt : features)
    {
        cout << wkt << endl;
    }

    return true;
}

static bool load_features_wkb(const vector<bytes>& features)
{
    WKBReader wkb_reader;
    for (const bytes& wkb : features)
    {
        buffer_info wkb_info(buffer(wkb).request());
        unsigned char* wkb = reinterpret_cast<unsigned char*>(wkb_info.ptr);
        size_t wkb_size = static_cast<size_t>(wkb_info.size);
        string wkb_text(reinterpret_cast<char*>(wkb), wkb_size);
        stringstream wkb_stream;
        wkb_stream << wkb_text;
        unique_ptr<Geometry> geometry = wkb_reader.read(wkb_stream);
        cout << geometry->toText() << endl;
    }

    return true;
}

PYBIND11_MODULE(geo, py_module)
{
    py_module.doc() = "Geometry module for various geospatial workflows.";

    py_module.def("from_wkt", &load_features_wkt, "Load features represented as WKT.");
    py_module.def("from_wkb", &load_features_wkb, "Load features represented as WKB.");
}