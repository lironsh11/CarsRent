import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'orderby'
})
export class OrderbyPipe implements PipeTransform {

  transform(sortedArray: any[]): unknown {

    if (sortedArray != undefined) {
      for (let i = 0; i < sortedArray.length; i++) {
        parseInt(sortedArray[i].manufacturerYear);
      }
      var swapp;
      var arrLength = sortedArray.length - 1;
      do {
        swapp = false;
        for (var i = 0; i < arrLength; i++) {
          if (sortedArray[i].manufacturerYear > sortedArray[i + 1].manufacturerYear) {
            var temp = sortedArray[i].manufacturerYear;
            sortedArray[i].manufacturerYear = sortedArray[i + 1].manufacturerYear;
            sortedArray[i + 1].manufacturerYear = temp;
            swapp = true;
          }
        }
        arrLength--;
      } while (swapp);
    }
    return sortedArray;
  }




}


