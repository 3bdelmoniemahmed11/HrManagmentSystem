import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-print-form',
  templateUrl: './print-form.component.html',
  styleUrls: [],
})
export class PrintFormComponent implements OnInit, AfterViewInit, OnDestroy {
  employeeData: any;
  isPrinted: boolean = false;

  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit() {
    this.employeeData = history.state.data;
    this.employeeData = JSON.parse(this.employeeData);
    console.log(this.employeeData); // Check the contents of the object
    console.log(this.employeeData.id); // Access the id property
    console.log(this.employeeData.employee.name); // Access the name property
  }

  ngAfterViewInit() {
    // Attach event listener for the 'afterprint' event
    window.addEventListener('afterprint', () => this.handleAfterPrint());
  }

  ngOnDestroy() {
    // Remove the event listener when the component is destroyed
    window.removeEventListener('afterprint', () => this.handleAfterPrint());
  }

  handleAfterPrint = () => {
    window.removeEventListener('afterprint', this.handleAfterPrint);
    this.isPrinted = false; // Reset the flag to false after printing
    location.reload(); // Refresh the page
  };

  printSection() {
    if (!this.isPrinted) {
      const printContents = document.getElementById('printSection')?.innerHTML;
      const originalContents = document.body.innerHTML;
      if (printContents) {
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
        this.isPrinted = true; // Set the flag to true after printing
        window.addEventListener('afterprint', this.handleAfterPrint);
      }
    }
  }

  getDateNow() {
    var currentDate = new Date();

    // Format the date as desired
    var formattedDate = currentDate.toDateString();
    return formattedDate;
  }
}
