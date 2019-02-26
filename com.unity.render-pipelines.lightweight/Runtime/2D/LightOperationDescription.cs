using System;

namespace UnityEngine.Experimental.Rendering.LWRP
{
    [Serializable]
    public struct _2DLightOperationDescription
    {
        internal enum TextureChannel
        {
            None, R, G, B, A
        }

        internal struct MaskChannelFilter
        {
            public Vector4 mask { get; private set; }
            public Vector4 inverted { get; private set; }

            public MaskChannelFilter(Vector4 m, Vector4 i)
            {
                mask = m;
                inverted = i;
            }
        }

        internal enum BlendMode
        {
            Additive = 0,
            Modulate = 1,
            Subtractive = 2,
            Custom = 99
        }

        [Serializable]
        internal struct BlendFactors
        {
            [SerializeField] internal float modulate;
            [SerializeField] internal float additve;
        }

        public bool enabled;
        public string name;
        [ColorUsageAttribute(false, true)]
        [SerializeField] internal Color globalColor;
        [SerializeField] internal string maskTextureChannel;
        [SerializeField] internal BlendMode blendMode;
        [SerializeField] internal float renderTextureScale;
        [SerializeField] internal BlendFactors customBlendFactors;



        internal Vector2 blendFactors
        {
            get
            {
                var result = new Vector2();

                switch (blendMode)
                {
                    case BlendMode.Additive:
                        result.x = 0.0f;
                        result.y = 1.0f;
                        break;
                    case BlendMode.Modulate:
                        result.x = 1.0f;
                        result.y = 0.0f;
                        break;
                    case BlendMode.Subtractive:
                        result.x = 0.0f;
                        result.y = -1.0f;
                        break;
                    case BlendMode.Custom:
                        result.x = customBlendFactors.modulate;
                        result.y = customBlendFactors.additve;
                        break;
                    default:
                        result = Vector2.zero;
                        break;
                }

                return result;
            }
        }

        internal MaskChannelFilter maskTextureChannelFilter
        {
            get
            {
                switch (maskTextureChannel)
                {
                    case "R":
                        return new MaskChannelFilter(new Vector4(1, 0, 0, 0), new Vector4(0, 0, 0, 0));
                    case "1-R":
                        return new MaskChannelFilter(new Vector4(1, 0, 0, 0), new Vector4(1, 0, 0, 0));
                    case "G":
                        return new MaskChannelFilter(new Vector4(0, 1, 0, 0), new Vector4(0, 0, 0, 0));
                    case "1-G":
                        return new MaskChannelFilter(new Vector4(0, 1, 0, 0), new Vector4(0, 1, 0, 0));
                    case "B":
                        return new MaskChannelFilter(new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 0));
                    case "1-B":
                        return new MaskChannelFilter(new Vector4(0, 0, 1, 0), new Vector4(0, 0, 1, 0));
                    case "A":
                        return new MaskChannelFilter(new Vector4(0, 0, 0, 1), new Vector4(0, 0, 0, 0));
                    case "1-A":
                        return new MaskChannelFilter(new Vector4(0, 0, 0, 1), new Vector4(0, 0, 0, 1));
                    case "None":
                    default:
                        return new MaskChannelFilter(Vector4.zero, Vector4.zero);
                }
            }
        }
    }
}
