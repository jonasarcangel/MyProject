import { Component, OnInit, Inject } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { User } from "../../../../entities/user";
import { UsersService } from "../../services/users.service";

@Component({
  selector: "app-admin-users-home",
  templateUrl: "./home.component.html",
  providers: [UsersService]
})
export class AdminUsersHomeComponent implements OnInit {
  public users: User[];

  constructor(@Inject(UsersService) public usersService: UsersService) {}

  ngOnInit() {
    this.users = new Array();
    this.usersService.getAll().subscribe(users => this.setUsers(users));
  }

  private setUsers(users: User[]) {
    this.users = users;
  }

  // public addSection() {
  //     this.sectionService.AddSection(this.section)
  //         .subscribe(section => this.addSectionSuccess(section));
  // }

  // private addSectionSuccess(section: Section) {
  //     if (section.id !== '0') {
  //         alert('Saved.');
  //         this.section = section;
  //         this.sections.push(this.section);
  //         this.section = new Section();
  //     }
  //     else {
  //         alert('Save failed.');
  //     }
  // }

  private SaveResponse(data: any) {
    if (data !== null) {
      if (data.value !== null) {
        if (data.value === "0") {
          alert("Saved.");
        } else {
          alert("Save failed.");
        }
      } else {
        alert("Save failed.");
      }
    } else {
      alert("Save failed.");
    }
  }
}
