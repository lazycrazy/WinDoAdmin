using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace WinDo.Utilities
{
    /// <summary>
    /// ImageHelper
    /// 图片处理类
    /// </summary>
    public class ImageHelper
    {

        public static System.Drawing.Image resizeImageHold(System.Drawing.Image imgToResize, Size size)
        {
            OrientationImage(imgToResize);
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercent);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        /// <summary>
        /// 缩放图像
        /// </summary>
        /// <param name="image"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Image ZoomImage(Image image, float scale)
        {
            if (image == null)
                throw new ArgumentNullException("image cannot be null");
            if (scale <= 0)
                throw new ArgumentException("scale must be more than zero");
            Bitmap bmp = new Bitmap((int)Math.Ceiling(image.Width * scale), (int)Math.Ceiling(image.Height * scale));
            Bitmap bmpOld = image.Clone() as Bitmap;
            BitmapData bmpDataNew = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            BitmapData bmpDataOld = bmpOld.LockBits(new Rectangle(Point.Empty, image.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] byColorNew = new byte[bmpDataNew.Height * bmpDataNew.Stride];
            byte[] byColorOld = new byte[bmpDataOld.Height * bmpDataOld.Stride];
            Marshal.Copy(bmpDataOld.Scan0, byColorOld, 0, byColorOld.Length);
            for (int x = 0, lenX = bmpDataNew.Width; x < lenX; x++)
            {
                int srcX = (int)(x / scale) << 2;
                for (int y = 0, lenY = bmpDataNew.Height; y < lenY; y++)
                {
                    int offsetOld = (int)(y / scale) * bmpDataOld.Stride + srcX;
                    int offsetNew = y * bmpDataNew.Stride + (x << 2);
                    if (offsetOld < 0) offsetOld = 0;
                    else if (offsetOld >= byColorOld.Length) offsetOld = byColorOld.Length - 1;
                    if (offsetNew < 0) offsetNew = 0;
                    else if (offsetNew >= byColorNew.Length) offsetNew = byColorNew.Length - 1;
                    byColorNew[offsetNew] = byColorOld[offsetOld];
                    byColorNew[offsetNew + 1] = byColorOld[offsetOld + 1];
                    byColorNew[offsetNew + 2] = byColorOld[offsetOld + 2];
                    byColorNew[offsetNew + 3] = byColorOld[offsetOld + 3];
                }
            }
            bmpOld.UnlockBits(bmpDataOld);
            Marshal.Copy(byColorNew, 0, bmpDataNew.Scan0, byColorNew.Length);
            bmp.UnlockBits(bmpDataNew);
            bmpOld.Dispose();
            return bmp;
        }

        /// <summary>
        /// 马赛克
        /// </summary>
        /// <param name="imgSrc"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Image Mosaic(Image imgSrc, int width)
        {
            Bitmap bmp = ((Bitmap)imgSrc).Clone(new Rectangle(Point.Empty, imgSrc.Size), imgSrc.PixelFormat);
            BitmapData bmpData = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            byte[] byColorInfoSrc = new byte[bmpData.Height * bmpData.Stride];
            Marshal.Copy(bmpData.Scan0, byColorInfoSrc, 0, byColorInfoSrc.Length);
            int indexB = 0;
            int indexG = 0;
            int indexR = 0;
            int rCount = 0;
            for (int x = 0, lenx = bmp.Width; x < lenx; x += width)
            {
                for (int y = 0, leny = bmp.Height; y < leny; y += width)
                {
                    int r = 0, g = 0, b = 0; rCount = 0;
                    for (int tempx = x, lentx = x + width <= lenx ? x + width : lenx; tempx < lentx; tempx++)
                    {
                        for (int tempy = y, lenty = y + width <= leny ? y + width : leny; tempy < lenty; tempy++)
                        {
                            indexB = tempy * bmpData.Stride + tempx * 3;
                            indexG = indexB + 1;
                            indexR = indexB + 2;

                            b += byColorInfoSrc[indexB];
                            g += byColorInfoSrc[indexG];
                            r += byColorInfoSrc[indexR];
                            rCount++;
                        }
                    }
                    for (int tempx = x, lentx = x + width <= lenx ? x + width : lenx; tempx < lentx; tempx++)
                    {
                        for (int tempy = y, lenty = y + width <= leny ? y + width : leny; tempy < lenty; tempy++)
                        {
                            indexB = tempy * bmpData.Stride + tempx * 3;
                            indexG = indexB + 1;
                            indexR = indexB + 2;

                            byColorInfoSrc[indexB] = (byte)(b / (rCount));
                            byColorInfoSrc[indexG] = (byte)(g / (rCount));
                            byColorInfoSrc[indexR] = (byte)(r / (rCount));
                        }
                    }
                }
            }
            Marshal.Copy(byColorInfoSrc, 0, bmpData.Scan0, byColorInfoSrc.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        public static void OrientationImage(Image image)
        {
            if (Array.IndexOf(image.PropertyIdList, 274) <= -1) return;
            var orientation = (int)image.GetPropertyItem(274).Value[0];
            switch (orientation)
            {
                case 1:
                    // No rotation required.
                    break;
                case 2:
                    image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                case 3:
                    image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 4:
                    image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case 5:
                    image.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;
                case 6:
                    image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 7:
                    image.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;
                case 8:
                    image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
            image.RemovePropertyItem(274);
        }



        public static Image resizeImage(Image imgToResize, Size size)
        {
            if (imgToResize == null) return null;
            return (Image)(new Bitmap(imgToResize, size));
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Image ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


        #region Base64转换成图片上传到FTP
        public Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to base 64 string
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        #endregion

        #region 裁剪圆形图片
        //裁剪圆形图片
        public static Image CutEllipse(Image img, Rectangle rec, Size size)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                using (TextureBrush br = new TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp, rec))
                {
                    br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(br, new Rectangle(Point.Empty, size));
                }
            }
            return bitmap;
        }
        #endregion


        /// <summary>
        /// 将Image转化为byte数组
        /// </summary>
        /// <param name="imageIn">图片</param>
        /// <returns></returns>
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {

            if (imageIn == null)
                return null;
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        /// <summary>
        /// 将byte数组转化为Image
        /// </summary>
        /// <param name="byteArrayIn">图片数组</param>
        /// <returns></returns>
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            if (byteArrayIn == null || byteArrayIn.Length == 0)
                return null;
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// 将图片缩放为指定大小
        /// </summary>
        /// <param name="originalImagePath">源图片路径</param>
        /// <param name="thumbnailPath">缩放后图片存放路径</param>
        /// <param name="width">缩放图片宽</param>
        /// <param name="height">缩放图片高</param>
        /// <param name="zoomtype">缩放类型</param>
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ZoomType zoomtype)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (zoomtype)
            {
                case ZoomType.HW://指定高宽缩放（可能变形）                
                    break;
                case ZoomType.W://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ZoomType.H://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ZoomType.Cut://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);
            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 将图片缩放为指定大小
        /// </summary>
        /// <param name="originalImagePath">源图片路径</param>
        /// <param name="width">缩放图片宽</param>
        /// <param name="height">缩放图片高</param>
        /// <param name="mode">缩放类型</param>
        /// <returns></returns>
        public static Image RetrunImage(string originalImagePath, int width, int height, ZoomType zoomtype)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (zoomtype)
            {
                case ZoomType.HW://指定高宽缩放（可能变形）                
                    break;
                case ZoomType.W://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case ZoomType.H://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case ZoomType.Cut://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);
            //g.Clear(ColorTranslator.FromHtml("#fffbd9"));
            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                return bitmap;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                // bitmap.Dispose();  一定不能有 不然返回图片出错
                g.Dispose();
            }
        }


        /// <summary>
        /// 以逆时针为方向对图像进行旋转
        /// </summary>
        /// <param name="b">位图流</param>
        /// <param name="angle">旋转角度[0,360](前台给的)</param>
        /// <returns></returns>
        public static Bitmap Rotate(Bitmap b, int angle)
        {
            angle = angle % 360; //弧度转换
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);
            //原图的宽和高
            int w = b.Width;
            int h = b.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));
            //目标位图
            Bitmap dsImage = new Bitmap(W, H);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dsImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //计算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);
            //构造图像显示区域：让图像的中心与窗口的中心点一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);
            //恢复图像在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(b, rect);
            //重至绘图的所有变换
            g.ResetTransform();
            g.Save();
            g.Dispose();
            //dsImage.Save("yuancd.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return dsImage;
        }


        /// <summary>
        /// 缩略图，按高度和宽度来缩略
        /// </summary>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Image Scale(Image image, Size size)
        {
            return image.GetThumbnailImage(size.Width, size.Height, null, new IntPtr());
        }

        /// <summary>
        /// 缩略图，按倍数来缩略
        /// </summary>
        /// <param name="image">原图</param>
        /// <param name="multiple">放大或缩小的倍数，负数表示缩小，正数表示放大</param>
        /// <returns></returns>
        public static Image Scale(Image image, Int32 multiple)
        {
            Int32 newWidth;
            Int32 newHeight;

            Int32 absMultiple = Math.Abs(multiple);

            if (multiple == 0)
            {
                return image.Clone() as Image;
            }

            if (multiple < 0)
            {
                newWidth = image.Width / absMultiple;
                newHeight = image.Height / absMultiple;
            }
            else
            {
                newWidth = image.Width * absMultiple;
                newHeight = image.Height * absMultiple;
            }

            return image.GetThumbnailImage(newWidth, newHeight, null, new IntPtr());
        }

        /// <summary>
        /// 固定宽度缩略
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static Image ScaleFixWidth(Image image, Int32 width)
        {
            Int32 newWidth = width;
            Int32 newHeight;

            Double tempMultiple = (Double)newWidth / (Double)image.Width;

            newHeight = (Int32)(((Double)image.Height) * tempMultiple);

            Image newImage = new Bitmap(newWidth, newHeight);

            using (Graphics newGp = Graphics.FromImage(newImage))
            {
                newGp.CompositingQuality = CompositingQuality.HighQuality;

                //设置高质量插值法
                newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //设置高质量,低速度呈现平滑程度
                newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                newGp.Clear(Color.Transparent);

                newGp.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));

            }

            return newImage;

        }

        /// <summary>
        /// 固定高度缩略
        /// </summary>
        /// <param name="image"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image ScaleFixHeight(Image image, Int32 height)
        {
            Int32 newWidth;
            Int32 newHeight = height;



            Double tempMultiple = (Double)newHeight / (Double)image.Height;

            newWidth = (Int32)(((Double)image.Width) * tempMultiple);

            Image newImage = new Bitmap(newWidth, newHeight);

            using (Graphics newGp = Graphics.FromImage(newImage))
            {
                newGp.CompositingQuality = CompositingQuality.HighQuality;

                //设置高质量插值法
                newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //设置高质量,低速度呈现平滑程度
                newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                newGp.Clear(Color.Transparent);

                newGp.DrawImage(image, new Rectangle(0, 0, newWidth, newHeight));

            }

            return newImage;

        }

        /// <summary>
        /// 裁减缩略，根据固定的高度和宽度
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image ScaleCut(Image image, Int32 width, Int32 height)
        {
            int x = 0;
            int y = 0;
            int ow = image.Width;
            int oh = image.Height;
            if (width >= ow && height >= oh)
            {
                return image;
            }

            //如果结果要比原来的宽
            if (width > ow)
            {
                width = ow;
            }

            if (height > oh)
            {
                height = oh;
            }


            if ((double)image.Width / (double)image.Height > (double)width / (double)height)
            {
                oh = image.Height;
                ow = image.Height * width / height;
                y = 0;
                x = (image.Width - ow) / 2;
            }
            else
            {
                ow = image.Width;
                oh = image.Width * height / width;
                x = 0;
                y = (image.Height - oh) / 2;
            }

            Image newImage = new Bitmap(width, height);

            using (Graphics newGp = Graphics.FromImage(newImage))
            {
                newGp.CompositingQuality = CompositingQuality.HighQuality;

                //设置高质量插值法
                newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //设置高质量,低速度呈现平滑程度
                newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //清空画布并以透明背景色填充
                newGp.Clear(Color.Transparent);

                newGp.DrawImage(image, new Rectangle(0, 0, width, height),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            }

            return newImage;

        }


        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, ow, oh),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 打水印，在某一点
        /// </summary>
        /// <param name="image"></param>
        /// <param name="waterImagePath"></param>
        /// <param name="p"></param>
        public static void Makewater(Image image, String waterImagePath, Point p)
        {
            ImageHelper.Makewater(image, waterImagePath, p, ImagePosition.TopLeft);
        }

        public static void Makewater(Image image, String waterImagePath, Point p, ImagePosition imagePosition)
        {
            using (Image warterImage = Image.FromFile(waterImagePath))
            {
                using (Graphics newGp = Graphics.FromImage(image))
                {
                    newGp.CompositingQuality = CompositingQuality.HighQuality;

                    //设置高质量插值法
                    newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    //设置高质量,低速度呈现平滑程度
                    newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    switch (imagePosition)
                    {
                        case ImagePosition.BottomLeft:
                            p.Y = image.Height - warterImage.Height - p.Y;
                            break;
                        case ImagePosition.TopRigth:
                            p.X = image.Width - warterImage.Width - p.X;
                            break;
                        case ImagePosition.BottomRight:
                            p.Y = image.Height - warterImage.Height - p.Y;
                            p.X = image.Width - warterImage.Width - p.X;
                            break;
                    }

                    newGp.DrawImage(warterImage, new Rectangle(p, new Size(warterImage.Width, warterImage.Height)));

                }
            }
        }

        public static void Makewater(Image image, String waterStr, Font font, Brush brush, Point p)
        {
            ImageHelper.Makewater(image, waterStr, font, brush, p, ImagePosition.TopLeft);
        }

        public static void Makewater(Image image, String waterStr, Font font, Brush brush, Point p, ImagePosition imagePosition)
        {

            using (Graphics newGp = Graphics.FromImage(image))
            {
                Int32 stringWidth;
                Int32 stringHeight;
                stringHeight = (int)font.Size;
                //stringWidth = (int)(((float)StringDeal.GetBitLength(waterStr) / (float)2) * (font.Size + 1));
                stringWidth = (int)((font.Size + 1));

                newGp.CompositingQuality = CompositingQuality.HighQuality;

                //设置高质量插值法
                newGp.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                //设置高质量,低速度呈现平滑程度
                newGp.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //文字抗锯齿
                newGp.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                switch (imagePosition)
                {
                    case ImagePosition.BottomLeft:
                        p.Y = image.Height - stringHeight - p.Y;
                        break;
                    case ImagePosition.TopRigth:
                        p.X = image.Width - stringWidth - p.X;
                        break;
                    case ImagePosition.BottomRight:
                        p.Y = image.Height - stringHeight - p.Y;
                        p.X = image.Width - stringWidth - p.X;
                        break;
                }

                newGp.DrawString(waterStr, font, brush, p);

            }
        }

        /// <summary>
        /// 高质量保存      
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path"></param>
        public static void SaveQuality(Image image, String path)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = ImageCodecInfo.GetImageEncoders()[0];
            myEncoder = Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L); // 0-100
            myEncoderParameters.Param[0] = myEncoderParameter;
            try
            {
                image.Save(path, myImageCodecInfo, myEncoderParameters);
            }
            finally
            {
                myEncoderParameter.Dispose();
                myEncoderParameters.Dispose();
            }
        }

        #region public static void CutForSquare(System.Web.HttpPostedFile postedFile, string fileSaveUrl, int side, int quality):正方型裁剪并缩放
        /// <summary>  
        /// 正方型裁剪  
        /// 以图片中心为轴心，截取正方型，然后等比缩放  
        /// 用于头像处理  
        /// </summary>  
        /// <remarks></remarks>  
        /// <param name="postedFile">原图HttpPostedFile对象</param>  
        /// <param name="fileSaveUrl">缩略图存放地址</param>  
        /// <param name="side">指定的边长（正方型）</param>  
        /// <param name="quality">质量（范围0-100）</param>  
        public static void CutForSquare(System.Web.HttpPostedFile postedFile, string fileSaveUrl, int side, int quality)
        {
            //创建目录  
            string dir = Path.GetDirectoryName(fileSaveUrl);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）  
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(postedFile.InputStream, true);

            //原图宽高均小于模版，不作处理，直接保存  
            if (initImage.Width <= side && initImage.Height <= side)
            {
                initImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //原始图片的宽、高  
                int initWidth = initImage.Width;
                int initHeight = initImage.Height;

                //非正方型先裁剪为正方型  
                if (initWidth != initHeight)
                {
                    //截图对象  
                    System.Drawing.Image pickedImage = null;
                    System.Drawing.Graphics pickedG = null;

                    //宽大于高的横图  
                    if (initWidth > initHeight)
                    {
                        //对象实例化  
                        pickedImage = new System.Drawing.Bitmap(initHeight, initHeight);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                        //设置质量  
                        pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        //定位  
                        Rectangle fromR = new Rectangle((initWidth - initHeight) / 2, 0, initHeight, initHeight);
                        Rectangle toR = new Rectangle(0, 0, initHeight, initHeight);
                        //画图  
                        pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                        //重置宽  
                        initWidth = initHeight;
                    }
                    //高大于宽的竖图  
                    else
                    {
                        //对象实例化  
                        pickedImage = new System.Drawing.Bitmap(initWidth, initWidth);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                        //设置质量  
                        pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        //定位  
                        Rectangle fromR = new Rectangle(0, (initHeight - initWidth) / 2, initWidth, initWidth);
                        Rectangle toR = new Rectangle(0, 0, initWidth, initWidth);
                        //画图  
                        pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                        //重置高  
                        initHeight = initWidth;
                    }

                    //将截图对象赋给原图  
                    initImage = (System.Drawing.Image)pickedImage.Clone();
                    //释放截图资源  
                    pickedG.Dispose();
                    pickedImage.Dispose();
                }

                //缩略图对象  
                System.Drawing.Image resultImage = new System.Drawing.Bitmap(side, side);
                System.Drawing.Graphics resultG = System.Drawing.Graphics.FromImage(resultImage);
                //设置质量  
                resultG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                resultG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //用指定背景色清空画布  
                resultG.Clear(Color.White);
                //绘制缩略图  
                resultG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, side, side), new System.Drawing.Rectangle(0, 0, initWidth, initHeight), System.Drawing.GraphicsUnit.Pixel);

                //关键质量控制  
                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff  
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

                //保存缩略图  
                resultImage.Save(fileSaveUrl, ici, ep);

                //释放关键质量控制所用资源  
                ep.Dispose();

                //释放缩略图资源  
                resultG.Dispose();
                resultImage.Dispose();

                //释放原始图片资源  
                initImage.Dispose();
            }
        }
        #endregion

        #region public static void CutForSquare(System.IO.Stream fromFile, string fileSaveUrl, int side, int quality):正方型裁剪
        /// <summary>  
        /// 正方型裁剪  
        /// 以图片中心为轴心，截取正方型，然后等比缩放  
        /// 用于头像处理  
        /// </summary>  
        /// <remarks></remarks>  
        /// <param name="postedFile">原图HttpPostedFile对象</param>  
        /// <param name="fileSaveUrl">缩略图存放地址</param>  
        /// <param name="side">指定的边长（正方型）</param>  
        /// <param name="quality">质量（范围0-100）</param>  
        public static void CutForSquare(System.IO.Stream fromFile, string fileSaveUrl, int side, int quality)
        {
            //创建目录  
            string dir = Path.GetDirectoryName(fileSaveUrl);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）  
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(fromFile, true);

            //原图宽高均小于模版，不作处理，直接保存  
            if (initImage.Width <= side && initImage.Height <= side)
            {
                initImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //原始图片的宽、高  
                int initWidth = initImage.Width;
                int initHeight = initImage.Height;

                //非正方型先裁剪为正方型  
                if (initWidth != initHeight)
                {
                    //截图对象  
                    System.Drawing.Image pickedImage = null;
                    System.Drawing.Graphics pickedG = null;

                    //宽大于高的横图  
                    if (initWidth > initHeight)
                    {
                        //对象实例化  
                        pickedImage = new System.Drawing.Bitmap(initHeight, initHeight);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                        //设置质量  
                        pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        //定位  
                        Rectangle fromR = new Rectangle((initWidth - initHeight) / 2, 0, initHeight, initHeight);
                        Rectangle toR = new Rectangle(0, 0, initHeight, initHeight);
                        //画图  
                        pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                        //重置宽  
                        initWidth = initHeight;
                    }
                    //高大于宽的竖图  
                    else
                    {
                        //对象实例化  
                        pickedImage = new System.Drawing.Bitmap(initWidth, initWidth);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                        //设置质量  
                        pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        //定位  
                        Rectangle fromR = new Rectangle(0, (initHeight - initWidth) / 2, initWidth, initWidth);
                        Rectangle toR = new Rectangle(0, 0, initWidth, initWidth);
                        //画图  
                        pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                        //重置高  
                        initHeight = initWidth;
                    }

                    //将截图对象赋给原图  
                    initImage = (System.Drawing.Image)pickedImage.Clone();
                    //释放截图资源  
                    pickedG.Dispose();
                    pickedImage.Dispose();
                }

                //缩略图对象  
                System.Drawing.Image resultImage = new System.Drawing.Bitmap(side, side);
                System.Drawing.Graphics resultG = System.Drawing.Graphics.FromImage(resultImage);
                //设置质量  
                resultG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                resultG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //用指定背景色清空画布  
                resultG.Clear(Color.White);
                //绘制缩略图  
                resultG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, side, side), new System.Drawing.Rectangle(0, 0, initWidth, initHeight), System.Drawing.GraphicsUnit.Pixel);

                //关键质量控制  
                //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff  
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

                //保存缩略图  
                resultImage.Save(fileSaveUrl, ici, ep);

                //释放关键质量控制所用资源  
                ep.Dispose();

                //释放缩略图资源  
                resultG.Dispose();
                resultImage.Dispose();

                //释放原始图片资源  
                initImage.Dispose();
            }
        }
        #endregion

        #region 固定模版裁剪并缩放:public static void CutForCustom(System.Web.HttpPostedFile postedFile, string fileSaveUrl, int maxWidth, int maxHeight, int quality):\
        /// <summary>  
        /// 指定长宽裁剪  
        /// 按模版比例最大范围的裁剪图片并缩放至模版尺寸  
        /// </summary>  
        /// <param name="postedFile">原图HttpPostedFile对象</param>  
        /// <param name="fileSaveUrl">保存路径</param>  
        /// <param name="maxWidth">最大宽(单位:px)</param>  
        /// <param name="maxHeight">最大高(单位:px)</param>  
        /// <param name="quality">质量（范围0-100）</param>  
        public static void CutForCustom(System.Web.HttpPostedFile postedFile, string fileSaveUrl, int maxWidth, int maxHeight, int quality)
        {
            //从文件获取原始图片，并使用流中嵌入的颜色管理信息  
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(postedFile.InputStream, true);

            //原图宽高均小于模版，不作处理，直接保存  
            if (initImage.Width <= maxWidth && initImage.Height <= maxHeight)
            {
                initImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //模版的宽高比例  
                double templateRate = (double)maxWidth / maxHeight;
                //原图片的宽高比例  
                double initRate = (double)initImage.Width / initImage.Height;

                //原图与模版比例相等，直接缩放  
                if (templateRate == initRate)
                {
                    //按模版大小生成最终图片  
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
                    templateImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                //原图与模版比例不等，裁剪后缩放  
                else
                {
                    //裁剪对象  
                    System.Drawing.Image pickedImage = null;
                    System.Drawing.Graphics pickedG = null;

                    //定位  
                    Rectangle fromR = new Rectangle(0, 0, 0, 0);//原图裁剪定位  
                    Rectangle toR = new Rectangle(0, 0, 0, 0);//目标定位  

                    //宽为标准进行裁剪  
                    if (templateRate > initRate)
                    {
                        //裁剪对象实例化  
                        pickedImage = new System.Drawing.Bitmap(initImage.Width, (int)Math.Floor(initImage.Width / templateRate));
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        //裁剪源定位  
                        fromR.X = 0;
                        fromR.Y = (int)Math.Floor((initImage.Height - initImage.Width / templateRate) / 2);
                        fromR.Width = initImage.Width;
                        fromR.Height = (int)Math.Floor(initImage.Width / templateRate);

                        //裁剪目标定位  
                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = initImage.Width;
                        toR.Height = (int)Math.Floor(initImage.Width / templateRate);
                    }
                    //高为标准进行裁剪  
                    else
                    {
                        pickedImage = new System.Drawing.Bitmap((int)Math.Floor(initImage.Height * templateRate), initImage.Height);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        fromR.X = (int)Math.Floor((initImage.Width - initImage.Height * templateRate) / 2);
                        fromR.Y = 0;
                        fromR.Width = (int)Math.Floor(initImage.Height * templateRate);
                        fromR.Height = initImage.Height;

                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = (int)Math.Floor(initImage.Height * templateRate);
                        toR.Height = initImage.Height;
                    }

                    //设置质量  
                    pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //裁剪  
                    pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);

                    //按模版大小生成最终图片  
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);

                    //关键质量控制  
                    //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff  
                    ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo ici = null;
                    foreach (ImageCodecInfo i in icis)
                    {
                        if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                        {
                            ici = i;
                        }
                    }
                    EncoderParameters ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

                    //保存缩略图  
                    templateImage.Save(fileSaveUrl, ici, ep);
                    //templateImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);  

                    //释放资源  
                    templateG.Dispose();
                    templateImage.Dispose();

                    pickedG.Dispose();
                    pickedImage.Dispose();
                }
            }

            //释放资源  
            initImage.Dispose();
        }
        #endregion

        #region 等比缩放:public static void ZoomAuto(System.Web.HttpPostedFile postedFile, string savePath, System.Double targetWidth, System.Double targetHeight, string watermarkText, string watermarkImage)
        /// <summary>  
        /// 图片等比缩放  
        /// </summary>  
        /// <param name="postedFile">原图HttpPostedFile对象</param>  
        /// <param name="savePath">缩略图存放地址</param>  
        /// <param name="targetWidth">指定的最大宽度</param>  
        /// <param name="targetHeight">指定的最大高度</param>  
        /// <param name="watermarkText">水印文字(为""表示不使用水印)</param>  
        /// <param name="watermarkImage">水印图片路径(为""表示不使用水印)</param>  
        public static void ZoomAuto(System.Web.HttpPostedFile postedFile, string savePath, System.Double targetWidth, System.Double targetHeight, string watermarkText, string watermarkImage)
        {
            //创建目录  
            string dir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）  
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(postedFile.InputStream, true);

            //原图宽高均小于模版，不作处理，直接保存  
            if (initImage.Width <= targetWidth && initImage.Height <= targetHeight)
            {
                //文字水印  
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(initImage))
                    {
                        System.Drawing.Font fontWater = new Font("黑体", 10);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //透明图片水印  
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //获取水印图片  
                        using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                        {
                            //水印绘制条件：原始图片宽高均大于或等于水印图片  
                            if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(initImage);

                                //透明属性  
                                ImageAttributes imgAttributes = new ImageAttributes();
                                ColorMap colorMap = new ColorMap();
                                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = { colorMap };
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements = {
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5  
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                };

                                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage, new Rectangle(initImage.Width - wrImage.Width, initImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);

                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //保存  
                initImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //缩略图宽、高计算  
                double newWidth = initImage.Width;
                double newHeight = initImage.Height;

                //宽大于高或宽等于高（横图或正方）  
                if (initImage.Width > initImage.Height || initImage.Width == initImage.Height)
                {
                    //如果宽大于模版  
                    if (initImage.Width > targetWidth)
                    {
                        //宽按模版，高按比例缩放  
                        newWidth = targetWidth;
                        newHeight = initImage.Height * (targetWidth / initImage.Width);
                    }
                }
                //高大于宽（竖图）  
                else
                {
                    //如果高大于模版  
                    if (initImage.Height > targetHeight)
                    {
                        //高按模版，宽按比例缩放  
                        newHeight = targetHeight;
                        newWidth = initImage.Width * (targetHeight / initImage.Height);
                    }
                }

                //生成新图  
                //新建一个bmp图片  
                System.Drawing.Image newImage = new System.Drawing.Bitmap((int)newWidth, (int)newHeight);
                //新建一个画板  
                System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);

                //设置质量  
                newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //置背景色  
                newG.Clear(Color.White);
                //画图  
                newG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);

                //文字水印  
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(newImage))
                    {
                        System.Drawing.Font fontWater = new Font("宋体", 10);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //透明图片水印  
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //获取水印图片  
                        using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                        {
                            //水印绘制条件：原始图片宽高均大于或等于水印图片  
                            if (newImage.Width >= wrImage.Width && newImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(newImage);

                                //透明属性  
                                ImageAttributes imgAttributes = new ImageAttributes();
                                ColorMap colorMap = new ColorMap();
                                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = { colorMap };
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements = {
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5  
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                };

                                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage, new Rectangle(newImage.Width - wrImage.Width, newImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);
                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //保存缩略图  
                newImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                //释放资源  
                newG.Dispose();
                newImage.Dispose();
                initImage.Dispose();
            }
        }

        #endregion

        #region 其它
        /// <summary>  
        /// 判断文件类型是否为WEB格式图片  
        /// (注：JPG,GIF,BMP,PNG)  
        /// </summary>  
        /// <param name="contentType">HttpPostedFile.ContentType</param>  
        /// <returns></returns>  
        public static bool IsWebImage(string contentType)
        {
            if (contentType == "image/pjpeg" || contentType == "image/jpeg" || contentType == "image/gif" || contentType == "image/bmp" || contentType == "image/png")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion 
    }

    /// <summary>
    /// 缩放类型
    /// </summary>
    public enum ZoomType
    {
        /// <summary>
        /// 指定高宽缩放（可能变形）  
        /// </summary>
        HW,
        /// <summary>
        /// 指定高，宽按比例
        /// </summary>
        H,
        /// <summary>
        /// 指定宽，高按比例 
        /// </summary>
        W,
        /// <summary>
        /// 指定高宽裁减（不变形）  
        /// </summary>
        Cut
    }

    public enum StringPosition
    {
        TopLeft,
        BottomLeft
    }

    public enum ImagePosition
    {
        TopLeft,
        BottomLeft,
        BottomRight,
        TopRigth
    }
}
