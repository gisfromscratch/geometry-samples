# Enable gcc c++11 features
#QMAKE_CXXFLAGS += -std=c++11
QMAKE_CXXFLAGS += -std=c++0x

TEMPLATE = app
CONFIG += console
#CONFIG += c++11
CONFIG -= qt

QMAKE_CXXFLAGS += -std=c++0x

SOURCES += main.cpp \
    simplegeometry.cpp \
    simplespatialreference.cpp

HEADERS += \
    simplegeometry.h \
    simplespatialreference.h

