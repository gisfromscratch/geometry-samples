#include <iostream>

#include "simplespatialreference.h"

using namespace std;

int main()
{
    cout << "Geometry Algorithms ..." << endl;
    auto wgs84 = SimpleSpatialReference(SimpleWkid::WGS1984);
    cout << wgs84.toString() <<  endl;
    return 0;
}

