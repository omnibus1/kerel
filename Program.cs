// using SixLabors.ImageSharp;
// using SixLabors.ImageSharp.Processing;

// using (Image<Rgb24> image = Image.Load<Rgb24>("lena.png")) 
// {
//     // klon obrazka - pracujemy teraz na kopii danych
//     Image<Rgb24>my_clone = image.Clone();
//     // pÄtla po wszystkich pikselach
//     for (int a = 0; a < image.Width; a++)
//         for (int b = 0; b < image.Height; b++)
//         {
//             // pobranie skĹadnikĂłw RGB 
//             byte R = image[a,b].R;
//             byte G = image[a,b].G;
//             byte B = image[a,b].B;
//             byte avg=Convert.ToByte((R+B+G)/3);
//             Byte nr=Convert.ToByte(R/3);
//             Byte ng=Convert.ToByte(G/3);
//             Byte nb=Convert.ToByte(B/3);
//             // zmiana RGB na BGR
//             my_clone[a,b] = new Rgb24(avg, avg, avg);
//         }
//     //zapisanie obrazkĂłw
//     image.Save("test.png");
//     my_clone.Save("test2czarny2.png");           
// }



using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Collections;

using (Image<Rgb24> image = Image.Load<Rgb24>("lena2.png")) 
{
    // klon obrazka - pracujemy teraz na kopii danych
    int height=3;
    int width=3;
    int[,] verticalKernel = new int[3,3] {{ 1, 0,-1 }, 
                                    { 1,0,-1},
                                    { 1,-0,-1 }};

    int[,] horizontalKernel = new int[3,3] {{ 1, 2,1 }, 
                                    { 0,0,0},
                                    { -1,-2,-1 }};
    Image<Rgb24>my_clone = image.Clone();
    // pÄtla po wszystkich pikselach

    for (int a = width/2; a < image.Width-width/2; a++){
        for (int b = height/2; b < image.Height-height/2; b++)
        {

            int redV=0;
            int greenV=0;
            int blueV=0;
            int redH=0;
            int greenH=0;
            int blueH=0;
            
            for(int j=-height/2;j<height/2+1;j++){
                for(int i=-width/2;i<width/2+1;i++){
                    // Console.Write(b+j+"-");
                    // Console.Write(a+i+" ")
                    
                    // Console.WriteLine(image[a+i,b+j].R);
                    redH+=image[a+i,b+j].R*horizontalKernel[j+1,i+1];
                    blueH+=image[a+i,b+j].B*horizontalKernel[j+1,i+1];
                    greenH+=image[a+i,b+j].G*horizontalKernel[j+1,i+1];
                    redV+=image[a+i,b+j].R*verticalKernel[j+1,i+1];
                    blueV+=image[a+i,b+j].B*verticalKernel[j+1,i+1];
                    greenV+=image[a+i,b+j].G*verticalKernel[j+1,i+1];
  
                    // Console.Write(array2D[j+1,i+1]+"===");   
                    // Console.Write(i+1+"-");
                    // Console.Write(j+1);         

                }
                
                

             

            }
            if(redH>255)redH=255;
            if(redH<0)redH=0;
            if(greenH>255)greenH=255;
            if(greenH<0)greenH=0;
            if(blueH>255)blueH=255;
            if(blueH<0)blueH=0;

            if(redV>255)redV=255;
            if(redV<0)redV=0;
            if(greenV>255)greenV=255;
            if(greenV<0)greenV=0;
            if(blueV>255)blueV=255;
            if(blueV<0)blueV=0;

            int blue=Math.Max(blueH,blueV);
            int red=Math.Max(redH,redV);
            int green=Math.Max(greenH,greenV);

            my_clone[a,b] = new Rgb24(Convert.ToByte(blue), Convert.ToByte(green), Convert.ToByte(red));
            
            
            
        }
        
    }
    //zapisanie obrazkĂłw
    image.Save("test.png");
    my_clone.Save("kernel2.png");           
}
