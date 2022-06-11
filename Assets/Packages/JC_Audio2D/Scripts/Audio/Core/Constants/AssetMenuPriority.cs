// Author:  Joseph Crump
// Date:    05/14/22

namespace JC.Audio2D
{
    /// <summary>
    /// Order in which the scriptable objects appear in the CreateAsset menu.
    /// </summary>
    public class AssetMenuPriority
    {
        public const int SFX = 0;
        public const int Soundtrack = SFX + 1;
        public const int AudioFacilitator = 99;
    }
}
