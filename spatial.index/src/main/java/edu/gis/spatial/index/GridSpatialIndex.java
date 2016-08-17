package edu.gis.spatial.index;

/**
 * Represents a grid spatial index.
 * 
 * @author Jan Tschada
 *
 */
public class GridSpatialIndex {

	private final GridHierarchy hierarchy;

	/**
	 * Creates a new index instance using the specified hierarchy.
	 * 
	 * @param hierarchy
	 *            the hierarchy of this index.
	 */
	public GridSpatialIndex(GridHierarchy hierarchy) {
		this.hierarchy = hierarchy;
	}
}
