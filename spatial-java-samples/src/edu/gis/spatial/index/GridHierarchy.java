package edu.gis.spatial.index;

/**
 * Created by Developer on 16.05.2016.
 * @link https://msdn.microsoft.com/en-us/library/bb895265.aspx
 */
public class GridHierarchy {

    private final int numberOfLevels;

    /**
     * Creates a new grid hierarchy using the specified number of levels.
     * @param numberOfLevels The number of grid levels.
     */
    public GridHierarchy(int numberOfLevels) {
        if (numberOfLevels < 1) {
            throw new IllegalArgumentException("The number of levels must not be less than 1!");
        }

        this.numberOfLevels = numberOfLevels;
    }


}
