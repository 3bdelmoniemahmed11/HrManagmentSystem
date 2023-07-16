import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { permissionService } from 'src/app/Services/Permissions/Permission.service';
import { pagePermission } from '../Models/GroupPermission';
import Swal from 'sweetalert2';
import { GroupService } from 'src/app/Services/Group/Group.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-group',
  templateUrl: './add-group.component.html',
  styleUrls: []
})
export class AddGroupComponent {
  Pages:any;
  searchTerm:string;
  Actions:any;
  selectedPermissions: { [key: string]: any } = {};
  group_name:string;
  myList:any[]=[];
  constructor(private PermissionService:permissionService,private groupService:GroupService,private fb: FormBuilder,private router:Router) {

  }


  ngOnInit(){
    this.PermissionService.getAllPages().subscribe({
      next: (result) => { this.Pages=result;
       console.log(result);

      },
      error: (err) => { console.log(err); }
    });

    this.PermissionService.getAllActions().subscribe({
      next: (result) => {
        this.Actions=result;
       console.log(result);
      },
      error: (err) => { console.log(err.err); }
    });
}

updateSelectedPermissions(pageName: string, event: Event, permission: keyof pagePermission) {
const element = event.target as HTMLInputElement;
const isChecked = element.checked;
  if (!this.selectedPermissions[pageName]) {
    this.selectedPermissions[pageName] = { Show: false, Add: false, Edit: false, Delete: false };
  }
  this.selectedPermissions[pageName][permission] = isChecked;
}

getSelectedPages(): string[] {
  return Object.keys(this.selectedPermissions)
    .filter(pageName => Object.values(this.selectedPermissions[pageName]).some(Boolean));
}

Submit() {
  if(this.getSelectedPages().length==0|| this.group_name ==null){
    Swal.fire({
      icon: 'error',
      title: 'Oops...',
      text: 'Enter  data!',
    });

  }
  {
    // "groupName": "string",
    // "myList": [
    //   {
    //     "pageActionID": 0,
    //     "pageID": 0
    //   }
    // ]
  }

  const selectedPages = this.getSelectedPages();

  for (let index = 0; index < selectedPages.length; index++) {
    const ID = this.Pages.find((item: { id:number, name: string; }) => item.name == selectedPages[index]).id;

    const element =   this.selectedPermissions[selectedPages[index]];

    if(element.Show){ this.myList.push({pageActionID:1,pageID:ID});}
    if(element.Add){ this.myList.push({pageActionID:2,pageID:ID});}
    if(element.Edit){ this.myList.push({pageActionID:3,pageID:ID});}
    if(element.Delete){this.myList.push({pageActionID:4,pageID:ID});}

  }
  var data={groupName:this.group_name,myList:this.myList};
  console.log(data);

  this.groupService.Add(data).subscribe({
    next:(result) =>{
      Swal.fire({
            icon: 'success',
            title: 'Group Added',
            showConfirmButton: false,
            timer: 1500,
          });
          this.router.navigateByUrl('/permission');

        },
    error:(err)=>{
      console.log(err);
    }
  });
  // display selected pages in your application as needed
}
}
