namespace EAlbums
{
    public enum ViewPatterns
    {
        Pending = 0,
        Loading = 1,
        Ready = 2,
        Browse = 3,
    }
    public enum RevolveTypes
    {
        None = 0,
        Fixed = 1,
        Transformable = 2,
    }

    /// <summary>
    /// enumerations that specifies the zoom modes available when ever an image is on display
    /// </summary>

    public enum ZoomMode
    {
        FitPage,        //displays the entire image in the display region
        FitWidth,       //fits the image in the width of the control
        FitHeight,      //fits the image in the height of the control
        ActualSize      //displays the actual size of the image
    }

    /// <summary>
    /// enumerations that specifies how the mouse behaves over the control
    /// </summary>
    public enum PreviewMode
    { 
        None,
        RegionSelection,    //enables the user to select a region to zoom
        ZoomIn,             //enables the user to zoom in to a point
        ZoomOut,            //enables the user to zoom out to a point
        Pan,                //enables the user to grab and pan the image
     
    }
}
