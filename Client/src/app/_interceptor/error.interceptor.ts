import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const toastr = inject(ToastrService);

  return next(req).pipe(
    catchError(error => {
      if (error) {
        switch (error.status) {
          case 400:
            if(error.error.errors)
            {
              const modelStateErrors=[];
              for(const key in error.error.errors)
              {
                if(error.error.errors[key])
                {
                  modelStateErrors.push(error.error.errors[key])
                }
              }
              throw modelStateErrors.flat();
            }
            else
            {
              toastr.error(error.error,error.status);
            }

            break;
            case 401:
            toastr.error('Unauthorised',error.status);
            break;
            case 404:
            router.navigateByUrl('/Not-Found');
            break;
            case 500:
            const navExtra:NavigationExtras={state:{error:error.error}};
            router.navigateByUrl('/server-error',navExtra);
            break;
            default:
              toastr.error('something went wrong');
            break;
        }
      }
throw error;
      
    })
  );
};
