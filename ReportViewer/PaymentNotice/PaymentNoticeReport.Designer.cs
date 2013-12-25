namespace ReportViewer.PaymentNotice
{
    partial class PaymentNoticeReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule2 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.StyleRule styleRule3 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector1 = new Telerik.Reporting.Drawing.DescendantSelector();
            Telerik.Reporting.Drawing.StyleRule styleRule4 = new Telerik.Reporting.Drawing.StyleRule();
            Telerik.Reporting.Drawing.DescendantSelector descendantSelector2 = new Telerik.Reporting.Drawing.DescendantSelector();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.NoticeNumbers = new Telerik.Reporting.ObjectDataSource();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.shape1 = new Telerik.Reporting.Shape();
            this.panel1 = new Telerik.Reporting.Panel();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            this.AmountTextBox = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.PaymentNoticeLines = new Telerik.Reporting.ObjectDataSource();
            this.panel2 = new Telerik.Reporting.Panel();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox28 = new Telerik.Reporting.TextBox();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox32 = new Telerik.Reporting.TextBox();
            this.PaymentNotices = new Telerik.Reporting.ObjectDataSource();
            this.panel3 = new Telerik.Reporting.Panel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox22
            // 
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.899999737739563D));
            this.textBox22.StyleName = "Office.TableHeader";
            this.textBox22.Value = "发票类型";
            // 
            // textBox23
            // 
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.899999737739563D));
            this.textBox23.StyleName = "Office.TableHeader";
            this.textBox23.Value = "发票编号";
            // 
            // textBox24
            // 
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.899999737739563D));
            this.textBox24.StyleName = "Office.TableHeader";
            this.textBox24.Value = "金额";
            // 
            // textBox25
            // 
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.899999737739563D));
            this.textBox25.StyleName = "Office.TableHeader";
            this.textBox25.Value = "备注";
            // 
            // NoticeNumbers
            // 
            this.NoticeNumbers.DataMember = "GetNoticeNumber";
            this.NoticeNumbers.DataSource = "ReportViewer.PaymentNotice.PaymentNotice, ReportViewer, Version=1.0.0.0, Culture=" +
    "neutral, PublicKeyToken=null";
            this.NoticeNumbers.Name = "NoticeNumbers";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.5D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox3});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D), Telerik.Reporting.Drawing.Unit.Cm(1.2000000476837158D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.899899959564209D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.textBox1.Style.Font.Bold = true;
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.StyleName = "Header";
            this.textBox1.Value = "通知编号：{=Fields.NoticeNumber}";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.5D), Telerik.Reporting.Drawing.Unit.Cm(1.2000000476837158D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0000009536743164D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.textBox2.Style.Font.Bold = true;
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.StyleName = "Header";
            this.textBox2.Value = "创建日期：";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0:d}";
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.500201225280762D), Telerik.Reporting.Drawing.Unit.Cm(1.2000000476837158D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.6998000144958496D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.textBox3.Style.Font.Bold = true;
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.StyleName = "Header";
            this.textBox3.Value = "=Fields.CreateDate";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(7.8999996185302734D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.shape1,
            this.panel1,
            this.textBox5,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox14,
            this.textBox15,
            this.textBox16,
            this.textBox17,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.table1,
            this.panel2});
            this.detail.Name = "detail";
            // 
            // shape1
            // 
            this.shape1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010002215276472271D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.shape1.Name = "shape1";
            this.shape1.ShapeType = new Telerik.Reporting.Drawing.Shapes.LineShape(Telerik.Reporting.Drawing.Shapes.LineDirection.EW);
            this.shape1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(6.3779134750366211D), Telerik.Reporting.Drawing.Unit.Point(2D));
            // 
            // panel1
            // 
            this.panel1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox4});
            this.panel1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010002215276472271D), Telerik.Reporting.Drawing.Unit.Cm(0.070755936205387115D));
            this.panel1.Name = "panel1";
            this.panel1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.199901580810547D), Telerik.Reporting.Drawing.Unit.Cm(0.7292440533638D));
            this.panel1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel1.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010002215276472271D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.1998000144958496D), Telerik.Reporting.Drawing.Unit.Cm(0.72914391756057739D));
            this.textBox4.Style.Font.Bold = true;
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.StyleName = "Header";
            this.textBox4.Value = "付款通知信息";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.4997996985912323D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.700200080871582D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "供应商:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.2002003192901611D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9000000953674316D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "=Fields.SupplierName";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "银行账号：";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.6998000144958496D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "= Fields.BankAccountName";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D), Telerik.Reporting.Drawing.Unit.Cm(2.0999996662139893D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0999996662139893D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "币种：";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1999990940093994D), Telerik.Reporting.Drawing.Unit.Cm(2.0999996662139893D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7002005577087402D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "= Fields.CurrencyName";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.9004001617431641D), Telerik.Reporting.Drawing.Unit.Cm(2.0999996662139893D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0995979309082031D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "付款期限：";
            // 
            // textBox12
            // 
            this.textBox12.Format = "{0:d}";
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.0001983642578125D), Telerik.Reporting.Drawing.Unit.Cm(2.0999996662139893D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "=Fields.DeadLine";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.000398635864258D), Telerik.Reporting.Drawing.Unit.Cm(2.0999996662139893D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7996017932891846D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "经办人：";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.800200462341309D), Telerik.Reporting.Drawing.Unit.Cm(2.0999996662139893D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2801001071929932D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "=Fields.OperatorName";
            // 
            // textBox15
            // 
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.49999994039535522D), Telerik.Reporting.Drawing.Unit.Cm(3.2999999523162842D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0999996662139893D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "审核人：";
            // 
            // textBox16
            // 
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1999990940093994D), Telerik.Reporting.Drawing.Unit.Cm(3.2999999523162842D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7002007961273193D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox16.Value = "=Fields.Reviewer";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.9004001617431641D), Telerik.Reporting.Drawing.Unit.Cm(3.2999999523162842D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0995979309082031D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "审核日期：";
            // 
            // textBox18
            // 
            this.textBox18.Format = "{0:d}";
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.0001983642578125D), Telerik.Reporting.Drawing.Unit.Cm(3.2999999523162842D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "=Fields.ReviewDate";
            // 
            // textBox19
            // 
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.000398635864258D), Telerik.Reporting.Drawing.Unit.Cm(3.2999999523162842D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7996017932891846D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "状态：";
            // 
            // textBox20
            // 
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.800200462341309D), Telerik.Reporting.Drawing.Unit.Cm(3.2999999523162842D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2801001071929932D), Telerik.Reporting.Drawing.Unit.Cm(0.70000004768371582D));
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "= Fields.StatusString";
            // 
            // table1
            // 
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(0.899999737739563D)));
            this.table1.Body.SetCellContent(0, 0, this.textBox26);
            this.table1.Body.SetCellContent(0, 1, this.textBox27);
            this.table1.Body.SetCellContent(0, 2, this.AmountTextBox);
            this.table1.Body.SetCellContent(0, 3, this.textBox29);
            tableGroup1.ReportItem = this.textBox22;
            tableGroup2.ReportItem = this.textBox23;
            tableGroup3.ReportItem = this.textBox24;
            tableGroup4.ReportItem = this.textBox25;
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup2);
            this.table1.ColumnGroups.Add(tableGroup3);
            this.table1.ColumnGroups.Add(tableGroup4);
            this.table1.DataSource = this.PaymentNoticeLines;
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox26,
            this.textBox27,
            this.AmountTextBox,
            this.textBox29,
            this.textBox22,
            this.textBox23,
            this.textBox24,
            this.textBox25});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.5500004291534424D), Telerik.Reporting.Drawing.Unit.Cm(6D));
            this.table1.Name = "table1";
            tableGroup5.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup5.Name = "Detail";
            this.table1.RowGroups.Add(tableGroup5);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.59999942779541D), Telerik.Reporting.Drawing.Unit.Cm(1.799999475479126D));
            this.table1.StyleName = "Office.TableNormal";
            // 
            // textBox26
            // 
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.899999737739563D));
            this.textBox26.StyleName = "Office.TableBody";
            this.textBox26.Value = "=Fields.InvoiceTypeString";
            // 
            // textBox27
            // 
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.899999737739563D));
            this.textBox27.StyleName = "Office.TableBody";
            this.textBox27.Value = "=Fields.InvoiceNumber";
            // 
            // AmountTextBox
            // 
            this.AmountTextBox.Format = "{0:C2}";
            this.AmountTextBox.Name = "AmountTextBox";
            this.AmountTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.899999737739563D));
            this.AmountTextBox.StyleName = "Office.TableBody";
            this.AmountTextBox.Value = "=Fields.Amount";
            // 
            // textBox29
            // 
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6499998569488525D), Telerik.Reporting.Drawing.Unit.Cm(0.899999737739563D));
            this.textBox29.StyleName = "Office.TableBody";
            this.textBox29.Value = "=Fields.Note";
            // 
            // PaymentNoticeLines
            // 
            this.PaymentNoticeLines.DataMember = "GetPaymentNoticeLines";
            this.PaymentNoticeLines.DataSource = "ReportViewer.PaymentNotice.PaymentNotice, ReportViewer, Version=1.0.0.0, Culture=" +
    "neutral, PublicKeyToken=null";
            this.PaymentNoticeLines.Name = "PaymentNoticeLines";
            this.PaymentNoticeLines.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("noticeNumber", typeof(string), "=Parameters.noticeNumber.Value")});
            // 
            // panel2
            // 
            this.panel2.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox21});
            this.panel2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00020004430552944541D), Telerik.Reporting.Drawing.Unit.Cm(4.7000994682312012D));
            this.panel2.Name = "panel2";
            this.panel2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.080101013183594D), Telerik.Reporting.Drawing.Unit.Cm(0.99970072507858276D));
            this.panel2.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.panel2.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.2000001072883606D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.textBox21.Style.Font.Bold = true;
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.StyleName = "Header";
            this.textBox21.Value = "付款通知明细";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(4.3000006675720215D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox31,
            this.textBox32,
            this.panel3});
            this.pageFooterSection1.Name = "pageFooterSection1";
            // 
            // textBox28
            // 
            this.textBox28.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.5998001098632812D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox28.Name = "textBox28";
            this.textBox28.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9999992847442627D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.textBox28.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox28.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox28.Value = "总计：";
            // 
            // textBox30
            // 
            this.textBox30.Format = "{0:C2}";
            this.textBox30.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.1498003005981445D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9999992847442627D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.textBox30.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox30.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox30.Value = "=PageExec(\"AmountTextBox\",sum(Amount))";
            // 
            // textBox31
            // 
            this.textBox31.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.2999997138977051D), Telerik.Reporting.Drawing.Unit.Cm(1.3924989700317383D));
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.textBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox31.Value = "签字：";
            // 
            // textBox32
            // 
            this.textBox32.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(7.2999997138977051D), Telerik.Reporting.Drawing.Unit.Cm(2.5924983024597168D));
            this.textBox32.Name = "textBox32";
            this.textBox32.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3D), Telerik.Reporting.Drawing.Unit.Cm(1.0000003576278687D));
            this.textBox32.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox32.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox32.Value = "日期：";
            // 
            // PaymentNotices
            // 
            this.PaymentNotices.DataMember = "GetPaymentNotice";
            this.PaymentNotices.DataSource = "ReportViewer.PaymentNotice.PaymentNotice, ReportViewer, Version=1.0.0.0, Culture=" +
    "neutral, PublicKeyToken=null";
            this.PaymentNotices.Name = "PaymentNotices";
            this.PaymentNotices.Parameters.AddRange(new Telerik.Reporting.ObjectDataSourceParameter[] {
            new Telerik.Reporting.ObjectDataSourceParameter("noticeNumber", typeof(string), "=Parameters.noticeNumber.Value")});
            // 
            // panel3
            // 
            this.panel3.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox30,
            this.textBox28});
            this.panel3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00020004430552944541D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.panel3.Name = "panel3";
            this.panel3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.080101013183594D), Telerik.Reporting.Drawing.Unit.Cm(1.0001004934310913D));
            this.panel3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            // 
            // PaymentNoticeReport
            // 
            this.DataSource = this.PaymentNotices;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageHeaderSection1,
            this.detail,
            this.pageFooterSection1});
            this.Name = "PaymentNoticeReport";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D), Telerik.Reporting.Drawing.Unit.Mm(25.399999618530273D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AutoRefresh = true;
            reportParameter1.AvailableValues.DataSource = this.NoticeNumbers;
            reportParameter1.AvailableValues.DisplayMember = "= Fields.NoticeNumber";
            reportParameter1.AvailableValues.Sortings.Add(new Telerik.Reporting.Sorting("=Fields.NoticeNumber", Telerik.Reporting.SortDirection.Asc));
            reportParameter1.AvailableValues.ValueMember = "NoticeNumber";
            reportParameter1.Name = "noticeNumber";
            reportParameter1.Text = "通知编号";
            reportParameter1.Value = "= First(Fields.NoticeNumber)";
            reportParameter1.Visible = true;
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.Table), "Office.TableNormal")});
            styleRule2.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule2.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule2.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule2.Style.Font.Name = "Calibri";
            styleRule2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            descendantSelector1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Office.TableHeader")});
            styleRule3.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector1});
            styleRule3.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(167)))), ((int)(((byte)(227)))));
            styleRule3.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule3.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule3.Style.Color = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(34)))), ((int)(((byte)(77)))));
            styleRule3.Style.Font.Name = "Calibri";
            styleRule3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            styleRule3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            descendantSelector2.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.Table)),
            new Telerik.Reporting.Drawing.StyleSelector(typeof(Telerik.Reporting.ReportItem), "Office.TableBody")});
            styleRule4.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            descendantSelector2});
            styleRule4.Style.BorderColor.Default = System.Drawing.Color.Black;
            styleRule4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            styleRule4.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1D);
            styleRule4.Style.Font.Name = "Calibri";
            styleRule4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1,
            styleRule2,
            styleRule3,
            styleRule4});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(16.200000762939453D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.ObjectDataSource PaymentNotices;
        private Telerik.Reporting.ObjectDataSource NoticeNumbers;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.Shape shape1;
        private Telerik.Reporting.Panel panel1;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
        private Telerik.Reporting.TextBox AmountTextBox;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.ObjectDataSource PaymentNoticeLines;
        private Telerik.Reporting.TextBox textBox28;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox32;
        private Telerik.Reporting.Panel panel2;
        private Telerik.Reporting.Panel panel3;
    }
}