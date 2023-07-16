import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GeneralSettingSService } from 'src/app/Services/GeneralSetting/GeneralSettings.service';
import { WeeklyVacationsService } from 'src/app/Services/WeeklyVacations/WeeklyVacations.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-empholidayes',
  templateUrl: './empholidayes.component.html',
  styleUrls: []
})
export class EmpholidayesComponent implements OnInit {
  settingForm: FormGroup;
  Setting: any;
  modelWeekdays:any;
  weekdays = [
    { label: 'Monday', isSelected: false },
    { label: 'Tuesday', isSelected: false },
    { label: 'Wednesday', isSelected: false },
    { label: 'Thursday', isSelected: false },
    { label: 'Friday', isSelected: false },
    { label: 'Saturday', isSelected: false },
    { label: 'Sunday', isSelected: false }
  ];
  selectedWeekdays: string[] = [];
  selectedOld:string[] = [];
  old:any[]=[];
  new:any[]=[];


  constructor(private formBuilder: FormBuilder, private activatedRoute: ActivatedRoute,
    private route: Router, private Generalsetting: GeneralSettingSService,private WeeklyDays:WeeklyVacationsService) {}

  ngOnInit() {
    this.settingForm = this.formBuilder.group({
      Extra: [0],
      Discount: [0]

    });
    this.Generalsetting.getLastSetting().subscribe({
      next: (result) => {
        this.Setting = result;
        this.Setting = this.Setting;

        if (this.Setting != null) {
          this.settingForm.controls['Extra'].setValue(this.Setting.addationValue);
          this.settingForm.controls['Discount'].setValue(this.Setting.deductionValue);
        }
      },
      error: (err) => { console.log(err.err); }
    });

    this.WeeklyDays.GetDays().subscribe({
      next: (days) => {
        this.modelWeekdays=days;
        this.modelWeekdays= this.modelWeekdays;

        for (let index = 0; index < this.modelWeekdays.length; index++) {
          this.selectedWeekdays.push(this.modelWeekdays[index].dayName);
          this.selectedOld.push(this.modelWeekdays[index].dayName);

        }

        this.updateSelectedWeekdays();
      },
      error: (error)=>{
        console.log(error.err);

      }
    })
  }

  updateSelectedWeekdays() {
    for (let i = 0; i < this.weekdays.length; i++) {
      const weekday = this.weekdays[i];
      weekday.isSelected = this.selectedWeekdays.includes(weekday.label);
    }
  }

  onSubmit() {
    if (this.settingForm.invalid) {
      return;
    }


   this.WeeklyFunction();
   this.UpdateWDays();
   this.AddWDays();
   if(this.Setting==null){
    this.AddNSetting();
    Swal.fire({
      icon: 'success',
      title: 'Data Updated',
      showConfirmButton: false,
      timer: 1500,
    });
   }
   else if( this.Setting.deductionValue!=this.settingForm.controls['Discount'].value || this.Setting.addationValue!=this.settingForm.controls['Extra'].value){
    this.UpdateSetting(this.AddNSetting.bind(this)); // Pass AddNSetting function as a callback
    Swal.fire({
      icon: 'success',
      title: 'Data Updated',
      showConfirmButton: false,
      timer: 1500,
    });
 }

   if(this.old!=null || this.new!=null ){
    Swal.fire({
      icon: 'success',
      title: 'Data Updated',
      showConfirmButton: false,
      timer: 1500,
    });
    //window.location.reload();
  }

  }

  WeeklyFunction(){
    this.selectedWeekdays
    for (let index = 0; index < this.modelWeekdays.length; index++) {
      if(!(this.selectedWeekdays.includes(this.modelWeekdays[index].dayName))){
        this.modelWeekdays[index].endDate=new Date().toISOString().slice(0, 10);
        this.old.push(this.modelWeekdays[index]);
      }
    }
   console.log(this.selectedOld);

    for (let index = 0; index < this.selectedWeekdays.length; index++) {
      if(!this.selectedOld.includes(this.selectedWeekdays[index])){

        this.new.push({
          dayName:this.selectedWeekdays[index],
          startDate:new Date().toISOString().slice(0, 10),
          endDate:null});
      }
    }
  }

  UpdateWDays() {
   if(this.old.length>0){
    this.WeeklyDays.EditDays(this.old).subscribe({
      next: (res) => {
        console.log(res);

      },
      error: (err) => {
        console.log(err.err);
      }
    });
   }


  }

  async AddWDays() {
if(this.new.length>0){
  await this.WeeklyDays.AddDays(this.new).subscribe({
    next: (res) => {
      console.log(res);

    },
    error: (e) => { }
  });
}

  }
  onWeekdayChange() {
    this.selectedWeekdays = this.weekdays.filter((weekday) => weekday.isSelected).map((weekday) => weekday.label);

  }

  UpdateSetting(callback: () => void) {
    this.Setting.endDate=new Date().toISOString().slice(0, 10);
    this.Generalsetting.EditSetting(this.Setting).subscribe({
      next: (res) => {
        console.log(res);
        callback(); // Call the callback function
      },
      error: (err) => {
        console.log(err.err);
      }
    });
  }

  AddNSetting() {
    const newSetting = {
      startDate: new Date().toISOString().slice(0, 10),
      endDate: null,
      deductionValue: this.settingForm.controls['Discount'].value,
      addationValue: this.settingForm.controls['Extra'].value
    };

    this.Generalsetting.AddSetting(newSetting).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (e) => { console.log(e);
      }
    });
  }
}

