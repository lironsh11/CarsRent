import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'orderById'
})
export class OrderByIdPipe implements PipeTransform {

  transform(originalArray: any[], val: string): unknown {
    let currentIdArr=[]
    if (originalArray != undefined) {
    for (let i = 0; i < originalArray.length; i++) {
      if (originalArray[i].userId.includes(val)) {
        currentIdArr.push(originalArray[i]);
          }
          if(val == ""){
            return originalArray
          }
    }
    return currentIdArr;
  }
  }
}
