
// using SixLabors.ImageSharp;
// using SixLabors.ImageSharp.Processing;
// using System.Collections;

// using (Image<Rgb24> image = Image.Load<Rgb24>("lena2.png")) 
// {
//     // klon obrazka - pracujemy teraz na kopii danych
//     int height=3;
//     int width=3;
//     int[,] verticalKernel = new int[3,3] {{ 1, 0,-1 }, 
//                                     { 1,0,-1},
//                                     { 1,-0,-1 }};

//     int[,] horizontalKernel = new int[3,3] {{ 1, 2,1 }, 
//                                     { 0,0,0},
//                                     { -1,-2,-1 }};
//     Image<Rgb24>my_clone = image.Clone();
//     // pÄtla po wszystkich pikselach

//     for (int a = width/2; a < image.Width-width/2; a++){
//         for (int b = height/2; b < image.Height-height/2; b++)
//         {

//             int redV=0;
//             int greenV=0;
//             int blueV=0;
//             int redH=0;
//             int greenH=0;
//             int blueH=0;
            
//             for(int j=-height/2;j<height/2+1;j++){
//                 for(int i=-width/2;i<width/2+1;i++){
//                     // Console.Write(b+j+"-");
//                     // Console.Write(a+i+" ")
                    
//                     // Console.WriteLine(image[a+i,b+j].R);
//                     redH+=image[a+i,b+j].R*horizontalKernel[j+1,i+1];
//                     blueH+=image[a+i,b+j].B*horizontalKernel[j+1,i+1];
//                     greenH+=image[a+i,b+j].G*horizontalKernel[j+1,i+1];
//                     redV+=image[a+i,b+j].R*verticalKernel[j+1,i+1];
//                     blueV+=image[a+i,b+j].B*verticalKernel[j+1,i+1];
//                     greenV+=image[a+i,b+j].G*verticalKernel[j+1,i+1];
  
//                     // Console.Write(array2D[j+1,i+1]+"===");   
//                     // Console.Write(i+1+"-");
//                     // Console.Write(j+1);         

//                 }
                
                

             

//             }
//             if(redH>255)redH=255;
//             if(redH<0)redH=0;
//             if(greenH>255)greenH=255;
//             if(greenH<0)greenH=0;
//             if(blueH>255)blueH=255;
//             if(blueH<0)blueH=0;

//             if(redV>255)redV=255;
//             if(redV<0)redV=0;
//             if(greenV>255)greenV=255;
//             if(greenV<0)greenV=0;
//             if(blueV>255)blueV=255;
//             if(blueV<0)blueV=0;

//             int blue=Math.Max(blueH,blueV);
//             int red=Math.Max(redH,redV);
//             int green=Math.Max(greenH,greenV);

//             my_clone[a,b] = new Rgb24(Convert.ToByte(blue), Convert.ToByte(green), Convert.ToByte(red));
            
            
            
//         }
        
//     }
//     //zapisanie obrazkĂłw
//     image.Save("test.png");
//     my_clone.Save("kernel2.png");           
// }

class ImageEditor{
    static string outputName="";


    public static void Main(string[]args){
        Console.WriteLine("Enter the desired files name");
        string fileName = Console.ReadLine();
        Image<Rgb24> image = Image.Load<Rgb24>(fileName);
        Console.WriteLine("Enter output file name");
        outputName=Console.ReadLine();
        Console.WriteLine("Choose what type of kernel do you want");
        Console.WriteLine("1-horizontal, 2-vertical , 3-both at once");
        string option=Console.ReadLine();
        ProcessImage(option,image);

    }

    
    static int[,] verticalKernel = new int[3,3] {{ 1, 0,-1 }, 
                                                { 1,0,-1},
                                                { 1,-0,-1 }};

    static int[,] horizontalKernel = new int[3,3] {{ 1, 2,1 }, 
                                                    { 0,0,0},
                                                    { -1,-2,-1 }};

    static void ProcessImage(string option,Image<Rgb24> image){
        
        if(option=="1"){
            Console.WriteLine("horizontal kernel");
            horizontalKernelProcessing(image);
        }
        else if(option=="2"){
            Console.WriteLine("vertical kernel");
            verticalKernelProcessing(image);
        }
        else if(option=="3"){
            Console.WriteLine("both at once");
            bothKernelsProcessing(image);

        }
        
    }
    static int normalizeForByte(int x){
        if (x>255)x=255;
        else if (x<0)x=0;
        return x;
    }
    static void horizontalKernelProcessing(Image<Rgb24> image){
        Image<Rgb24> imageClone=image.Clone();
        int height=3;
        int width=3;
        for (int a = width/2; a < image.Width-width/2; a++){
            for (int b = height/2; b < image.Height-height/2; b++){
                int red=0;
                int blue=0;
                int green=0;
                for(int j=-height/2;j<height/2+1;j++){
                    for(int i=-width/2;i<width/2+1;i++){
                        red+=image[a+i,b+j].R*horizontalKernel[j+1,i+1];
                        blue+=image[a+i,b+j].B*horizontalKernel[j+1,i+1];
                        green+=image[a+i,b+j].G*horizontalKernel[j+1,i+1];
                    }
                }
                red=normalizeForByte(red);
                green=normalizeForByte(green);
                blue=normalizeForByte(blue);
                imageClone[a,b] = new Rgb24(Convert.ToByte(blue), Convert.ToByte(green), Convert.ToByte(red));

            }
        }
        imageClone.Save(outputName); 
    }

        static void verticalKernelProcessing(Image<Rgb24> image){
        Image<Rgb24> imageClone=image.Clone();
        int height=3;
        int width=3;
        for (int a = width/2; a < image.Width-width/2; a++){
            for (int b = height/2; b < image.Height-height/2; b++){
                int red=0;
                int blue=0;
                int green=0;
                for(int j=-height/2;j<height/2+1;j++){
                    for(int i=-width/2;i<width/2+1;i++){
                        red+=image[a+i,b+j].R*verticalKernel[j+1,i+1];
                        blue+=image[a+i,b+j].B*verticalKernel[j+1,i+1];
                        green+=image[a+i,b+j].G*verticalKernel[j+1,i+1];
                    }
                }
                red=normalizeForByte(red);
                green=normalizeForByte(green);
                blue=normalizeForByte(blue);
                imageClone[a,b] = new Rgb24(Convert.ToByte(blue), Convert.ToByte(green), Convert.ToByte(red));

            }
        }
        imageClone.Save(outputName); 
    }

        static void bothKernelsProcessing(Image<Rgb24> image){
        Image<Rgb24> imageClone=image.Clone();
        int height=3;
        int width=3;
        for (int a = width/2; a < image.Width-width/2; a++){
            for (int b = height/2; b < image.Height-height/2; b++){
                int []reds=new int[2]{0,0};
                int []blues=new int[2]{0,0};
                int []greens=new int[2]{0,0};

                for(int j=-height/2;j<height/2+1;j++){
                    for(int i=-width/2;i<width/2+1;i++){
                        for(int k=0;k<2;k++){
                            if(k==0){
                                reds[0]+=image[a+i,b+j].R*verticalKernel[j+1,i+1];
                                blues[0]+=image[a+i,b+j].B*verticalKernel[j+1,i+1];
                                greens[0]+=image[a+i,b+j].G*verticalKernel[j+1,i+1];
                            }
                            else{
                                reds[1]+=image[a+i,b+j].R*horizontalKernel[j+1,i+1];
                                blues[1]+=image[a+i,b+j].B*horizontalKernel[j+1,i+1];
                                greens[1]+=image[a+i,b+j].G*horizontalKernel[j+1,i+1];
                            }
                        }

                        
                    }
                }
                int red=normalizeForByte(reds.Max());
                int green=normalizeForByte(greens.Max());
                int blue=normalizeForByte(blues.Max());
                imageClone[a,b] = new Rgb24(Convert.ToByte(blue), Convert.ToByte(green), Convert.ToByte(red));

            }
        }
        imageClone.Save(outputName); 
    }
}