using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Application;

public class FileConverterService
{
    public static string PlaceHolder = "data:image;base64,iVBORw0KGgoAAAANSUhEUgAAAGQAAABkCAYAAABw4pVUAAAABmJLR0QA/wD/AP+gvaeTAAAEJElEQVR4nO3dv49MURTA8e8Ku4kIiUKCRKFRaFQqlYqCjdiQkCgp7Cr5Eyh1dHTb6VSikbAaK1HQiFixiSAhg8KuXcWbiTFz38977jtnZs43ec3uvDt35pP33szedNcDzPM/z5JoDvgAfgVnluUx8U8BnYLO7rQHnVGfk8Yl/II5ioDNkCI5iqLMMo6wDFzUnNek5isEcxWCOYjBHMZijKLUJvAb2Bn4XQvEtbiutd0NHMQbiKAZBHMUgSFF+oa9fUhBwlLolBwFHqVMrIOAoVWsNBBylSq2CgKOU1ToIOEpRKiDgKHmpgYCjhFIFAUcZTB0EHKU/EyDgKL3MgED2p0RW/8RoBlgAloAf3W0JmO/+TipTIGATZT/wkuHH3tuWu7eRyBwI2EKZoRijH0XiSDEJAnZQFijH6G1XBe7PLAjYQHlOdZBnAvdnGgT0X311qA7SEbg/8yCgi1IH5LvA/Y0ECOidvvyUVZAGyjzVQcb6oj4F7Az8vG2UGbKXtGUYy8C0wP2ZBNkC3AFeAXsCv28bZT/FKGP9xnAbsNg3vhWUabJT0jOyC30HeNr9mcSR0csUyHbgYeA+Hufc3sL7FOnMgOwCngTGXwUOF+yn/T5FOhMgu4AXgbHfAgcr7D9OKOogB4A3gXFfAftqjDMuKKogh4D3gTGXgN0NxhsHFDWQIwx/CcEm8AjYETHuqKOogBwDvgXGeAdsazBeXqOK0irIeeB3YP+bNcZoksSRMjcwRsojrRWQ68DGwH4bwLWas21aDMogRmqUpCBTZEfA4D5rwKVm821cE5Q8jJQoyUC2AHcDt/9FdmHXqM41Je+2qdfOkoBM8/+KbW/rAMfj5htdlSMldGSsAxcq7h+TOEjeiu1X4Gj8fEUqelKLMKrsH5soSN6K7QrZMomlYk9JqZb+xUBiV2w1Kvtyg8Ejo8r+sUeKCIjUiq1GeShlGEVHhjBKNEhoSvmQ7OI+SsVeE6SuKdEgS8iu2KZqjuz6VvVbjNo8fYmB3CbNiq10/e8z6ny1VFso0SAbwI2KE9QudEp5ia3TV7Q2T19iILdJs2IrXf/7jDpfLdUWSjTIBnCj4gS1m6X+E6SBEg0yKn2g2RPbNspEgtR95dMmysSAnCBDWaHZR7BtoUwMiERtvPpykJqlRnGQBqVEcZCGpbqmOEhEKZ7UHKRhqZ7UHCS+FEeKg0SWGqU0BxkuJUppDhIuFUppDpKfxIXeQYRT/zzEG0718xAvXFOUaBDf0v6nNqVpP3irWyqU0rQfuOUtBUpp2g/a+iaN4iVsVP8h6ljnKAZzFIM5isEcxWCOYrC8VeLTmpOa9EIoK6oz8oZQVnWn40F2mnpHdnScUp6L53meN0b9BWyZ6ZDczM2rAAAAAElFTkSuQmCC";

    public static string ConvertToBase64(Stream file, int w = 256)
    {
        if (file.Length > 0)
        {
            using var image = Image.Load(file);
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(w, (int)(w * image.Height / image.Width)),
                Mode = ResizeMode.Max
            }));

            using var ms = new MemoryStream();
            image.SaveAsPng(ms);
            return "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
        }
        else
        {
            throw new FileLoadException();
        }
    }
}