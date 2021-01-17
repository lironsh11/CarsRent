import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'search'
})
export class SearchPipe implements PipeTransform {

  transform(originalArr: any[], val: string): any[] {
    let resultsArr = []
    val = val.toString().toLowerCase();
    if (originalArr != undefined) {
      for (let i = 0; i < originalArr.length; i++) {
        if (originalArr[i].manufacturer.toLowerCase().includes(val)) {
          resultsArr.push(originalArr[i]);
        }
        if (originalArr[i].model.toLowerCase().includes(val)) {
          resultsArr.push(originalArr[i]);
        }
        if (originalArr[i].gearType.toLowerCase().includes(val)) {
          resultsArr.push(originalArr[i]);
        }
        if (originalArr[i].manufacturerYear.toLowerCase().includes(val)) {
          resultsArr.push(originalArr[i]);
        }
        if (val == "") {
          return originalArr
        }
      }
      return resultsArr
    }
  }
}
