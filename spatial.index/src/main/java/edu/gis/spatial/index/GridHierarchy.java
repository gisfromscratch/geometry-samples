package edu.gis.spatial.index;

import java.util.Collection;

/**
 * Created by Developer on 16.05.2016.
 * 
 * @link https://msdn.microsoft.com/en-us/library/bb895265.aspx
 */
public class GridHierarchy {

	private final Collection<GridLevel> levels;

	/**
	 * Creates a new grid hierarchy using the specified levels.
	 * 
	 * @param levels
	 *            the grid levels.
	 */
	public GridHierarchy(Collection<GridLevel> levels) {
		if (null == levels) {
			throw new IllegalArgumentException("The levels must not be null!");
		}
		if (levels.size() < 1) {
			throw new IllegalArgumentException("The number of levels must not be less than 1!");
		}

		this.levels = levels;
	}

}
