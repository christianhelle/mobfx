namespace ChristianHelle.Framework.WindowsMobile.Drawing.Native
{
    internal struct GRADIENT_RECT
    {
        private uint UpperLeft;
        private uint LowerRight;

        public GRADIENT_RECT(uint ul, uint lr)
        {
            UpperLeft = ul;
            LowerRight = lr;
        }
    }
}