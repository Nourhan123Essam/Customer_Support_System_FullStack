import { Component } from '@angular/core';
import { UserServiceService } from '../../Services/UserService/user-service.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-feedback',
  imports: [CommonModule,FormsModule, RouterModule],
  templateUrl: './add-feedback.component.html',
  styleUrl: './add-feedback.component.css'
})
export class AddFeedbackComponent {
  ticketId!: number;
  rating: number = 0;
  feedback: string = '';

  constructor(
    private userService: UserServiceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.ticketId = Number(this.route.snapshot.paramMap.get('id'));
  }

  setRating(value: number) {
    this.rating = value;
  }

  submitRating() {
    if (this.rating === 0) {
      alert('الرجاء اختيار تقييم بين 1 و 5');
      return;
    }

    this.userService.addRating({
      ticketId: this.ticketId,
      score: this.rating,
      feedback: this.feedback,
    }).subscribe({
      next: () => {
        alert('تم إضافة التقييم بنجاح!');
        this.router.navigate(['/my-tickets']);
      },
      error: (err) => {
       // alert('حدث خطأ أثناء إضافة التقييم: ' + err.message);
       this.router.navigate(['/my-tickets']);
      }
    });
  }
}
