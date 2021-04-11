import sys
sys.path.append('./out/build/x64-Debug/pygeom')
import geo

import numpy as np
from pygeos import Geometry, io

import unittest



class GeoTest(unittest.TestCase):
  
    # Loading WKT
    def test_wkt(self):
        p1 = Geometry('POINT (12.242330 51.830817)')
        loaded = geo.from_wkt([str(p1)])
        self.assertTrue(loaded, 'WKT was not loaded!')

        loaded = geo.from_wkt(np.array([str(p1)]))
        self.assertTrue(loaded, 'WKT was not loaded!')

    # Loading WKB
    def test_wkb(self):
        p1 = Geometry('POINT (12.242330 51.830817)')
        loaded = geo.from_wkb([io.to_wkb(p1)])
        self.assertTrue(loaded, 'WKB was not loaded!')

        loaded = geo.from_wkb(np.array([io.to_wkb(p1)]))
        self.assertTrue(loaded, 'WKB was not loaded!')


  
if __name__ == '__main__':
    unittest.main()
