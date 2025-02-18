using System;
using OrderBouncer.Application.DTOs;

namespace OrderBouncer.Application.Services.Extractor;

public class LineItemPropertyExtractor
{
    public void Extract(string includedString){

    }

    public IList<NoteAttribute[]> GroupImages(NoteAttribute[] properties){
        IEnumerable<NoteAttribute> imageProperties = properties.Where(p => p.Name.StartsWith("Your-Photo"));
        List<NoteAttribute[]> imagePropertyGroup = [];
        
        int currentElement = -1;
        foreach(NoteAttribute image in imageProperties){
            string[] parts = image.Name.Split("-");
            int position = Convert.ToInt32(parts[2]); //0, 1, 2, 3, 4, ...

            if(position == 0){
                currentElement++;
                imagePropertyGroup[currentElement] = [];
            }

            imagePropertyGroup[currentElement][position] = new(){Name = position.ToString(), Value = image.Value};
        }

        return imagePropertyGroup;
    }
}
