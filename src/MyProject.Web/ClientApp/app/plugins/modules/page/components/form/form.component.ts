﻿import { Component, OnInit, Inject } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { Page } from "../../entities/page";
import { PageService } from "../../services/page.service";

@Component({
  selector: "app-plugins-page-form",
  templateUrl: "./form.component.html",
  providers: [PageService]
})
export class PageFormComponent implements OnInit {
  public page: Page;
  private parentId: string;
  private sub: any;
  private isEdit: boolean = false;

  public constructor(
    @Inject(PageService) private pageService: PageService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  public ngOnInit() {
    this.page = new Page(null);
    this.sub = this.route.params.subscribe(async params => {
      var alias = params["alias"];
      if (alias) {
        this.pageService
          .getByAlias(alias)
          .subscribe(page => this.setPage(page));
      }
    });
  }

  private setPage(page: Page) {
    this.page = page;
    this.isEdit = true;
  }

  public save() {
    this.pageService
      .addOrUpdate(this.page)
      .subscribe(result => this.savePageSuccess(this.page));
  }

  private savePageSuccess(page: Page) {
    alert("Saved.");
    this.router.navigateByUrl("page/" + page.alias);
  }

  private SaveResponse(data: any) {
    if (data !== null) {
      if (data.value !== null) {
        if (data.value === "1") {
          alert("Saved.");
          this.router.navigateByUrl("page/" + this.page.alias);
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
