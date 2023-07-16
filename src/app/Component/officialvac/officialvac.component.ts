import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AnnualVacationsService } from 'src/app/Services/AnnualVacations/AnnualVacations.service';
import { GroupService } from 'src/app/Services/Group/Group.service';
import { AuthenticationService } from 'src/app/Services/Identity/authentication.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-officialvac',
  templateUrl: './officialvac.component.html',
  styleUrls: []
})
export class OfficialvacComponent {
AnnualVacation:any;
Status:string="Add";
AnnualForm: FormGroup;
submitted:boolean=false;
groupId:number;
permisions:any[]=[];
results:any;

constructor(private annualServices:AnnualVacationsService,private formBuilder:FormBuilder,private auth:AuthenticationService,
  private groupservice:GroupService){}

ngOnInit(): void {
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
  this.AnnualForm = this.formBuilder.group({
    name: ['', Validators.required],
    vdata: ['', Validators.required],
    id:['']
  });
    this.annualServices.getAnnual().subscribe({
      next : (result) => {this.AnnualVacation = result;
       },
      error : (err)=>{console.log(err);}
    });
  }
  onSubmit(){
    this.submitted=true;
    if (this.AnnualForm.invalid){
      return;
    }

    if(this.Status=="Add"){
      const AnnualData = {
        vcationName:this.AnnualForm.controls['name'].value,
        vacationDate:this.AnnualForm.controls['vdata'].value,
        startDate: new Date().toISOString().slice(0, 10),
        endDate: null,

      };
      this.annualServices.Add(AnnualData).subscribe({
        next: (res) => {
          console.log(res);
          Swal.fire({
            icon: 'success',
            title: 'Data Added',
            showConfirmButton: false,
            timer: 1500,
          });
        },
        error: (e) => { }
      });
    }

    else if(this.Status=="Edit"){
      const AnnualData = {
        id:this.AnnualForm.controls['id'].value,
        vcationName:this.AnnualForm.controls['name'].value,
        vacationDate:this.AnnualForm.controls['vdata'].value,
        startDate: new Date().toISOString().slice(0, 10),
        endDate: null,
      };

      this.annualServices.Update(AnnualData).subscribe({
        next: (res) => {
          Swal.fire({
            icon: 'success',
            title: 'Data Updated',
            showConfirmButton: false,
            timer: 1500,
          });
        },
        error: (e) => { }
      });
      this.Status="Add";
      this.ngOnInit();
    }
    window.location.reload();
   //this.submitted=false;
  }

  EditAnnual(annual:any,e:Event){
    e.preventDefault();
   this.AnnualForm.controls['name'].setValue(annual.vcationName);
   this.AnnualForm.controls['vdata'].setValue(annual.vacationDate.slice(0, 10));
   this.AnnualForm.controls['id'].setValue(annual.id);
   this.Status="Edit";
  }

  DeleteAnnual(annual:any,e:Event){
    e.preventDefault();
    const AnnualData = {
      id:annual.id,
      vcationName:annual.vcationName,
      vacationDate:annual.vacationDate,
      startDate: annual.startDate,
      endDate: new Date().toISOString().slice(0, 10),
    };
    Swal.fire({
      title: 'Are you sure you want to delete this annual vacation?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes'
    }).then((result) => {
      if (result.isConfirmed) {
        this.annualServices.Update(AnnualData).subscribe({
          next: (res) => {
            Swal.fire({
              icon: 'success',
              title: 'Data Deleted',
              showConfirmButton: false,
              timer: 1500,
            });
          },
          error: (e) => { }
        });
        window.location.reload();
      }
    })


  }
  cancel(){
    this.AnnualForm.controls['id'].setValue("");
    this.AnnualForm.controls['name'].setValue("");
    this.AnnualForm.controls['vdata'].setValue("");
    this.Status="Add";
    this.submitted=false;
  }
  IsADD(){
    for (let index = 0; index < this.permisions.length; index++) {
      const element = this.permisions[index];
      if(element.pageId=5){
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
      if(element.pageId==5){
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
      if(element.pageId==5){
        if(element.pageActionId==4){
          return true;
         }
       }
    }
    return false;

  }
}
