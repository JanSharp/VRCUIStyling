using UnityEngine;

namespace JanSharp
{
    /// <summary>
    /// <inheritdoc cref="UIStyleColorAttribute(string)"/>
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class UIStyleColorAttribute : PropertyAttribute
    {
        // See the attribute guidelines at
        //  http://go.microsoft.com/fwlink/?LinkId=85236

        readonly string colorFieldName;
        public string ColorFieldName => colorFieldName;

        /// <summary>
        /// <para>This attribute must be applied to a <see cref="string"/> field which is serialized by unity
        /// (so public or using <see cref="SerializeField"/>).</para>
        /// </summary>
        /// <param name="colorFieldName">The <c>nameof()</c> a <see cref="Color"/> field. Said field must also
        /// be serialized by unity. Can also use the <see cref="HideInInspector"/> attribute since that field
        /// gets auto populated through applying UI style, though for easy viewing of what the color got
        /// resolved to it can be useful to keep it visible in the inspector.</param>
        public UIStyleColorAttribute(string colorFieldName)
        {
            this.colorFieldName = colorFieldName;
        }
    }

    /// <summary>
    /// <inheritdoc cref="UIStyleSpriteAttribute(string)"/>
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class UIStyleSpriteAttribute : PropertyAttribute
    {
        // See the attribute guidelines at
        //  http://go.microsoft.com/fwlink/?LinkId=85236

        readonly string spriteFieldName;
        public string SpriteFieldName => spriteFieldName;

        /// <summary>
        /// <para>This attribute must be applied to a <see cref="string"/> field which is serialized by unity
        /// (so public or using <see cref="SerializeField"/>).</para>
        /// </summary>
        /// <param name="spriteFieldName">The <c>nameof()</c> a <see cref="Sprite"/> field. Said field must
        /// also be serialized by unity. Can also use the <see cref="HideInInspector"/> attribute since that
        /// field gets auto populated through applying UI style, though for easy viewing of what the color got
        /// resolved to it can be useful to keep it visible in the inspector.</param>
        public UIStyleSpriteAttribute(string spriteFieldName)
        {
            this.spriteFieldName = spriteFieldName;
        }
    }
}
