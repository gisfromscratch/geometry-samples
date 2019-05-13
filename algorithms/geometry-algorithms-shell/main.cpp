#include <iostream>

#include "simplespatialreference.h"
#include "simplegeometry.h"

using namespace std;

int main()
{
    cout << "Geometry Algorithms ..." << endl;
    auto wgs84 = SimpleSpatialReference(SimpleWkid::WGS1984);
    cout << wgs84.toString() << endl;

    cout << sizeof(wgs84) << "-" << 4 *sizeof(int) << endl;

    auto location = SimplePoint2d(wgs84, 10, 53);
    cout << location.toString() << endl;
    return 0;
}

