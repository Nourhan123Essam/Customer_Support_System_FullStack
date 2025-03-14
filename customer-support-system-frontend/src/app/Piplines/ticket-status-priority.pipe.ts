import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'ticketStatusPriority'
})
export class TicketStatusPriorityPipe implements PipeTransform {

  transform(value: string, type: 'priority' | 'status'): string {
    if (type === 'priority') {
      switch (value) {
        case 'Low':
          return 'منخفض';
        case 'Medium':
          return 'متوسط';
        case 'High':
          return 'مرتفع';
        default:
          return value;
      }
    }

    if (type === 'status') {
      switch (value) {
        case 'Open':
          return 'مفتوح';
        case 'InProgress':
          return 'قيد التنفيذ';
        case 'Resolved':
          return 'تم الحل';
        case 'Closed':
          return 'مغلق';
        default:
          return value;
      }
    }

    return value;
  }
}
