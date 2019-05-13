#include <iostream>
#include <vector>

#include "simplespatialreference.h"
#include "simplegeometry.h"

using namespace std;


static SimplePoint2d createWgs84Point(double x, double y)
{
    auto wgs84 = SimpleSpatialReference(SimpleWkid::WGS1984);
    return SimplePoint2d(wgs84, x, y);
}


int main()
{
    cout << "Geometry Algorithms ..." << endl;

    std::vector<SimplePoint2d> geometries;

    {
    auto wgs84 = SimpleSpatialReference(SimpleWkid::WGS1984);
    cout << wgs84.toString() << endl;

    cout << sizeof(wgs84) << "-" << 4 *sizeof(int) << endl;

    auto location = SimplePoint2d(wgs84, 10, 53);
    cout << location.toString() << endl;

    auto point = createWgs84Point(13, 51);
    cout << point.toString() << endl;

    geometries.push_back(location);
    geometries.push_back(point);

    }

    for (auto &&geometry : geometries)
    {
        cout << geometry.toString() << endl;
    }

    return 0;
}

