# CMakeList.txt : CMake project for pygeom, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

# Setup testing
enable_testing ()
find_package (GTest REQUIRED)
include_directories (${GTEST_INCLUDE_DIR})

# Add source to this project's executable.
add_executable (pygeom "pygeom.cpp" "pygeom.h")

# Link libraries
target_link_libraries (pygeom ${GTEST_LIBRARY_DEBUG} ${GTEST_MAIN_LIBRARY_DEBUG})

# Add tests
 add_test(AllTestsInMain pygeom)
