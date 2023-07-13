import { filter } from 'rxjs/operators';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { async, firstValueFrom, lastValueFrom } from 'rxjs';
import Swal from 'sweetalert2';
import * as XLSX from 'xlsx';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AttendanceService } from 'src/app/Services/Attendance/attendance.service';


@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: [],
  
})
export class AttendanceComponent implements OnInit {
  @ViewChild('fileInput') fileInputRef!: ElementRef;
  allEmpAtts: any;
  startDate: any;
  endDate: any;
  array:any=[];
  attendanceee : [];
formatDateForInput(date: string): string {

  let datee = new Date(date);
  const formattedDate = datee.toISOString();
  return formattedDate;
}


searchFormGroup = new FormGroup({

  startDateControl: new FormControl('', [ Validators.required]),
  endDateControl : new FormControl('',[Validators.required]),  
});


  constructor(private attService: AttendanceService ) {}
  ngOnInit(): void {
    this.attService.getAllEmpAttendance().subscribe({
      next: (res) => {
        this.allEmpAtts = res;
        this.filteredEmpAtts=res;
        
        this.totalItems=this.filteredEmpAtts.length;
        this.totalPages=Math.ceil(this.totalItems/this.itemsPerPage);

          let startIndex = (this.currentPage - 1) * this.itemsPerPage;
          let endIndex = Math.min(startIndex + this.itemsPerPage, this.totalItems);
          this.filteredEmpAtts = this.allEmpAtts.slice(startIndex, endIndex);   


      },
      error: (err) => {
        console.log(err);
      },
    });

  }





  Delete(id: number): void {
    Swal.fire({
      title: 'Are you sure you want to delete this Attendance record?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes',
    }).then((result) => {
      if (result.isConfirmed) {
        this.removeDeletedChoice(id);
        Swal.fire(
          'Deleted!',
          'Your Attendance record has been deleted.',
          'success'
        );
      }
    });
  }

 
  removeDeletedChoice(id: any): void {
    this.attService.DeleteAttRecord(id).subscribe({
      next: () => {
        this.allEmpAtts = this.allEmpAtts.filter((item: any) => item.id != id);
        this.filteredEmpAtts=this.allEmpAtts;

        this.totalItems =  this.filteredEmpAtts.length;
        this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
        this.currentPage = 1; 
        this.updateDisplayedEmpAtts();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
  


  ///////////////////////////////////////////////////
  onFileSelected(event: any) {
    const file: File = event.target.files[0];
    console.log("on selected");

    this.uploadExcelFile(file);
    this.fileInputRef.nativeElement.value = null;
  }


  uploadExcelFile(file: File) {
    console.log("upload");
    this.array=[];
  const fileReader: FileReader = new FileReader();
  fileReader.onload = (e) => {
    const data: ArrayBuffer | string | null = fileReader.result;
    const workbook = XLSX.read(data, { type: 'array' });
    const worksheet = workbook.Sheets[workbook.SheetNames[0]];
    const headers: any = XLSX.utils.sheet_to_json(worksheet, { header: 1 })[0];
    const jsonData = XLSX.utils.sheet_to_json(worksheet, { header: headers });
    const excelData = jsonData.slice(1);

    let Dateformat: any;
    for (let index = 0; index < excelData.length; index++) {
      this.array.push(excelData[index]);

      Dateformat = this.formatDateForInput(this.array[index].date);
      this.array[index].date = Dateformat;

 
    }

    (async () => {

     let b =await lastValueFrom(this.attService.addAttendances(this.array));


       let a= await lastValueFrom(this.attService.getAllEmpAttendance());
       this.allEmpAtts=a;
       this.filteredEmpAtts=a;
         
        this.totalItems=this.filteredEmpAtts.length;
        this.totalPages=Math.ceil(this.totalItems/this.itemsPerPage);
          this.currentPage=1;
          this.updateDisplayedEmpAtts();
  })();


  };

  fileReader.readAsArrayBuffer(file);

}

filteredEmpAtts:any;
//search by date 
SearchByDays(){
  if (this.searchFormGroup.invalid) {
    this.searchFormGroup.markAllAsTouched();
    return;
  }
  
  const start = new Date(this.getStartDate.value);
  const end = new Date(this.getEndDate.value);
  if (start > end) {
    this.searchFormGroup.setErrors({ dateRange: true });
    return;
  }
  this.filteredEmpAtts = this.allEmpAtts;
  this.filteredEmpAtts = this.allEmpAtts.filter((item: any) => {
    const itemDate = new Date(item.date);
    
    return itemDate >= start && itemDate <= end;
  });
  if(this.searchByEmpOrDeptNameFormgroup.controls.searchInput.value != "" && this.searchByEmpOrDeptNameFormgroup.controls.searchInput.value != null  ){
    let inputValue=this.empDeptSearch.value.toLowerCase();
    this.filteredEmpAtts = this.filteredEmpAtts.filter((item: any) => {
      return item.empName.toLowerCase().includes(inputValue)  || item.deptName.toLowerCase().includes(inputValue);
    });
  }

  this.totalItems =  this.filteredEmpAtts.length;
  this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
  this.currentPage = 1; 
  
  this.updateDisplayedEmpAtts();

    
}


showAll(){
  this.filteredEmpAtts=this.allEmpAtts;
  this.searchFormGroup.reset();
  this.searchByEmpOrDeptNameFormgroup.reset();

  this.totalItems =  this.filteredEmpAtts.length;
  this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
  this.currentPage = 1; 
  
  this.updateDisplayedEmpAtts();

}



get getStartDate():any{
  return this.searchFormGroup.controls.startDateControl;
}

get getEndDate():any{
  return this.searchFormGroup.controls.endDateControl;
}


//search by emp name or dept name

searchByEmpOrDeptNameFormgroup = new FormGroup({

  searchInput: new FormControl('', [ Validators.minLength(2)]),
});

get empDeptSearch():any{
  return this.searchByEmpOrDeptNameFormgroup.controls.searchInput;
}

filter:any;
searchByEmpOrDeptName(){
  
  if (this.searchByEmpOrDeptNameFormgroup.valid) {
    
    this.filteredEmpAtts = this.allEmpAtts;

    let inputValue=this.empDeptSearch.value.toLowerCase();
    console.log(inputValue);

    this.filteredEmpAtts = this.allEmpAtts.filter((item: any) => {
      return item.empName.toLowerCase().includes(inputValue)  || item.deptName.toLowerCase().includes(inputValue);
    });

    if((this.getStartDate.value != "" && this.getStartDate.value != null) && (this.getEndDate.value != "" && this.getEndDate.value != null )){
      
      const start = new Date(this.getStartDate.value);
      const end = new Date(this.getEndDate.value);
      this.filteredEmpAtts = this.filteredEmpAtts.filter((item: any) => {
        const itemDate = new Date(item.date);
        
        return itemDate >= start && itemDate <= end;
      });
    }
   
  this.totalItems =  this.filteredEmpAtts.length;
  this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
  this.currentPage = 1; 

  

  this.updateDisplayedEmpAtts();
  }

  
}




//pagination 
  currentPage: number = 1;
  itemsPerPage: number = 6;
  totalItems: number;
  totalPages: number
  updateDisplayedEmpAtts(): void {

    let startIndex = (this.currentPage - 1) * this.itemsPerPage;
    let endIndex = Math.min(startIndex + this.itemsPerPage, this.totalItems);
    this.filteredEmpAtts = this.filteredEmpAtts.slice(startIndex, endIndex);

  }
  
  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.filteredEmpAtts=this.allEmpAtts;
      this.updateDisplayedEmpAtts();
    }
  }

  getTotalPagesArray(): number[] {
    return Array.from({ length: this.totalPages }, (_, index) => index + 1);
  }


}
