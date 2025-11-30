import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail',
  standalone: true,
  imports: [],
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.css']
})
export class DetailComponent {

  id = "";
  constructor(private router: ActivatedRoute) {
    this.id = String(this.router.snapshot.paramMap.get('id'));
  }
}
