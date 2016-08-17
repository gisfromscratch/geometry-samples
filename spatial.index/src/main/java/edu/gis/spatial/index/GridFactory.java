package edu.gis.spatial.index;

import java.awt.geom.Rectangle2D;
import java.util.ArrayList;
import java.util.Collection;

/**
 * Represents a factory for creating different grids.
 * 
 * @author Jan Tschada
 *
 */
public class GridFactory {

	public GridSpatialIndex createLowDensityIndex(Rectangle2D bounds) {
		Collection<GridLevel> levels = new ArrayList<GridLevel>(4);
		GridLevel rootLevel = new GridLevel(bounds, 4, 4);
		levels.add(rootLevel);
		
		// TODO: Create all other levels and adjust the bounds

		return new GridSpatialIndex(new GridHierarchy(levels));
	}
}
