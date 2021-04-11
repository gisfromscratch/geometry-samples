import sys
sys.path.append('./out/build/x64-Debug/pygeom')
import geo

from pygeos import Geometry

import unittest



class GeoTest(unittest.TestCase):
  
    # Returns True or False. 
    def test_wkt(self):
        p1 = Geometry('POINT (12.242330 51.830817)')
        loaded = geo.load_features_wkt([str(p1)])
        self.assertTrue(loaded, 'WKT was not loaded!')


  
if __name__ == '__main__':
    unittest.main()
