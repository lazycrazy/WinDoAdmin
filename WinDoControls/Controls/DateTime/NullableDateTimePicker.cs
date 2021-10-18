using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace WinDoControls.Controls
{
    public class NullableDateTimePicker : DateTimePicker
    {
        const string NullableFormat = " ";
        bool isSelfSetting;
        string originalCustomFormat;
        bool originalCustomFormatInitialized;
        DateTimePickerFormat? originalFormat;

        bool IsNullableState
        {
            get { return Format == DateTimePickerFormat.Custom && CustomFormat == NullableFormat; }
        }

        void SetNullable(bool nullable)
        {
            if (!this.originalFormat.HasValue)
            {
                this.originalFormat = Format;
            }
            if (!this.originalCustomFormatInitialized)
            {
                this.originalCustomFormat = CustomFormat;
                this.originalCustomFormatInitialized = true;
            }

            this.isSelfSetting = true;
            Format = Nullable && nullable ? DateTimePickerFormat.Custom : this.originalFormat.Value;
            CustomFormat = Nullable && nullable ? NullableFormat : this.originalCustomFormat;
            this.isSelfSetting = false;
        }

        #region Properties

        public bool Nullable { get; set; }

        #region DefaultValue

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime? DefaultValue { get; private set; }

        Func<DateTime?> defaultValueSetter;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<DateTime?> DefaultValueSetter
        {
            get { return this.defaultValueSetter; }
            set
            {
                this.defaultValueSetter = value;
                GetDefaultValue();
            }
        }

        DateTime? GetDefaultValue()
        {
            DefaultValue = null;
            if (this.defaultValueSetter != null)
                DefaultValue = this.defaultValueSetter();
            if (Nullable
                && IsNullableState
                && DefaultValue.HasValue)
                base.Value = DefaultValue.Value;
            return DefaultValue;
        }

        #endregion

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DateTime? Value
        {
            get { return IsNullableState ? (DateTime?)null : base.Value; }
            set
            {
                if (!Nullable && !value.HasValue)
                {
                    value = DateTime.Now;
                }

                if (value.HasValue)
                {
                    SetNullable(!value.HasValue);
                    base.Value = value.Value;
                }
                else
                {
                    GetDefaultValue();
                    SetNullable(!value.HasValue);
                    base.OnValueChanged(EventArgs.Empty);
                }
            }
        }

        public new DateTimePickerFormat Format
        {
            get { return base.Format; }
            set
            {
                if (!this.isSelfSetting)
                    this.originalFormat = value;
                base.Format = value;
            }
        }

        public new string CustomFormat
        {
            get { return base.CustomFormat; }
            set
            {
                if (!this.isSelfSetting)
                {
                    this.originalCustomFormat = value;
                    this.originalCustomFormatInitialized = true;
                }
                base.CustomFormat = value;
            }
        }

        #endregion

        #region Events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            var ohasValue = Value.HasValue;
            this.Focus();
            SuspendNullable();
            base.OnMouseDown(e);
            var nhasValue = Value.HasValue;
            if (ohasValue != nhasValue)
                base.OnValueChanged(e);
        }


        public bool ReadOnly
        {
            get;
            set;
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (ReadOnly)
            {
                if (e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
                {
                    e.Handled = true;
                    return;
                }
            }
            base.OnKeyPress(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (Nullable)
            {
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                    Value = null;
                else
                    SuspendNullable();
            }
            base.OnKeyDown(e);
            //base.OnValueChanged(e);
        }

        public void SuspendNullable()
        {
            if (Nullable && IsNullableState)
            {
                GetDefaultValue();
                SetNullable(false);
            }
        }

        #endregion
    }
}