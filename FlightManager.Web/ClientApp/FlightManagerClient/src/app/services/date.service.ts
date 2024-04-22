import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DateService {
  toLocalTime(dateUTCStr: string): Date {
    let dateUTC = dateUTCStr + "Z";
    return new Date(dateUTC);
  }

  toISOString(dateLocalStr: string): string {
    return new Date(dateLocalStr).toISOString();
  }

  toLocalTimeInputString(dateUTCStr: string): string {
    const dateUTC: Date = this.toLocalTime(dateUTCStr);

    let day = dateUTC.getDate().toString();
    console.log(day);
    if(day.length === 1) day = "0" + day;

    let month = (dateUTC.getMonth() + 1).toString();
    if(month.length === 1) month = "0" + month;

    let year = dateUTC.getFullYear().toString();

    let hour = dateUTC.getHours().toString();
    if(hour.length === 1) hour = "0" + hour;

    let minute = dateUTC.getMinutes().toString();
    if(minute.length === 1) minute = "0" + minute;

    const localTimeInputString = `${year}-${month}-${day}T${hour}:${minute}`;

    return localTimeInputString;
  }
}
