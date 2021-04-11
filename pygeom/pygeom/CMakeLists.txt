# CMakeList.txt : CMake project for pygeom, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

# Setup pybind11
set (PYTHON_EXECUTABLE "C:/bin/Anaconda3/python.exe")
find_package(pybind11 REQUIRED)

# Setup GEOS
find_package(geos REQUIRED)

# Setup testing
enable_testing ()
find_package (GTest REQUIRED)
include_directories (${GTEST_INCLUDE_DIR} ${PYBIND11_INCLUDE_DIR})

# Add source to this project's executable.
add_executable (pygeom "pygeom.cpp" "pygeom.h")

# Add python module
pybind11_add_module (geo "pymod.cpp")

# Link libraries for executable
target_link_libraries (pygeom PRIVATE ${GTEST_LIBRARY_DEBUG} ${GTEST_MAIN_LIBRARY_DEBUG})

# Link libraries for python module
target_link_libraries(geo PRIVATE GEOS::geos GEOS::geos_c GEOS::geos_cxx_flags)

# Add tests
 add_test(AllTestsInMain pygeom)
