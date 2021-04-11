// pygeom.cpp : Defines the entry point for the application.
//

#include "pygeom.h"

#include <gtest/gtest.h>

using namespace std;

TEST(load_features, wkt_test)
{
	EXPECT_EQ(1, 1);
}

int main(int argc, char **argv)
{
	::testing::InitGoogleTest(&argc, argv);
	return RUN_ALL_TESTS();
}
