import { Component, inject, Inject, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register-component',
  imports: [FormsModule],
  templateUrl: './register-component.component.html',
  styleUrl: './register-component.component.css'
})
export class RegisterComponentComponent {
  accountService=inject(AccountService)
  toast=inject(ToastrService)
  model:any={};
  CancelTheRegister=output<boolean>();

  register()
  {
    this.accountService.Register(this.model).subscribe({
      next: response => {
        console.log(response);
        this.Cancel();
      },
      error: error => this.toast.error(error.error)
    });
  }
  Cancel()
  {
    this.CancelTheRegister.emit(false);
  }
}
