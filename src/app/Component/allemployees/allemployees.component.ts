import { Component } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { filter } from 'rxjs';
import { GroupService } from 'src/app/Services/Group/Group.service';
import { AuthenticationService } from 'src/app/Services/Identity/authentication.service';

@Component({
  selector: 'app-allemployees',
  templateUrl: './allemployees.component.html',
  styleUrls: []
})
export class AllemployeesComponent {
  isLoginRoute = false;
  groupId:number;
  permisions:any[]=[];
  results:any;
  constructor(
    private router: Router,private auth:AuthenticationService,
    private groupservice:GroupService) {

  }


  ngOnInit(): void
  {
    this.auth.decodedToken();
    this.groupId = this.auth.getGroupIdFromToken();


    this.groupservice.getByID(this.groupId).subscribe({
      next:(result)=>{
        this.results= result;
        console.log(this.results);

        for (let index = 0; index < this.results.length; index++) {
          if(this.results[index].pageId==3){

          this.permisions.push(this.results[index]);
          }

        }

      },
      error:(err)=>{console.log(err);
      }
    })
  }
  //pageActionId
  IsADD(){
    for (let index = 0; index < this.permisions.length; index++) {
      const element = this.permisions[index];
      if(element.pageId=3){
        if(element.pageActionId=2){
          return true;
         }
       }

    }
    return false;

  }
  IsEdit(){
    for (let index = 0; index < this.permisions.length; index++) {
      const element = this.permisions[index];
      if(element.pageId==3){
        if(element.pageActionId==3){
          return true;
         }
       }
    }
    return false;

  }
  IsDelete(){
    for (let index = 0; index < this.permisions.length; index++) {
      const element = this.permisions[index];
      if(element.pageId==3){
        if(element.pageActionId==4){
          return true;
         }
       }
    }
    return false;

  }

}
