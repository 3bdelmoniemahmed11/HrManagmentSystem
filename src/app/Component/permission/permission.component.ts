import { Component } from '@angular/core';
import { GroupService } from 'src/app/Services/Group/Group.service';

@Component({
  selector: 'app-permission',
  templateUrl: './permission.component.html',
  styleUrls: []
})
export class PermissionComponent {
  Groups:any;
  searchTerm:string;

  constructor(private GroupService:GroupService){}

  ngOnInit(){
 this.GroupService.getAllGroup().subscribe({
      next: (result) => { this.Groups=result;
       console.log(result);

      },
      error: (err) => { console.log(err.err); }
    });
}

}
