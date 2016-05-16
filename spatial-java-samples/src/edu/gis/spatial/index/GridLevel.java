package edu.gis.spatial.index;

import java.awt.geom.Rectangle2D;

/**
 * Created by Developer on 16.05.2016.
 */
public class GridLevel {

    private final Rectangle2D envelope;
    private final int numberOfGridInX;
    private final int numberOfGridInY;

    /**
     * Creates a new grid level having the specified envelope.
     * @param envelope The envelope of this grid level.
     */
    public GridLevel(Rectangle2D envelope, int numberOfGridInX, int numberOfGridInY) {
        if (numberOfGridInX < 1) {
            throw new IllegalArgumentException("The number of grid must not be less than 1!");
        }
        if (numberOfGridInY < 1) {
            throw new IllegalArgumentException("The number of grid must not be less than 1!");
        }

        this.envelope = envelope;
        this.numberOfGridInX = numberOfGridInX;
        this.numberOfGridInY = numberOfGridInY;
    }
}
