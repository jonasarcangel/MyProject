﻿import { Component, OnInit, Inject } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { Faq } from "../../entities/faq";
import { Qa } from "../../entities/qa";
import { FaqService } from "../../services/faq.service";
import { ModalComponent } from "../../../../../shell/modal/modal.component";

@Component({
  selector: "app-plugins-faq-list-form",
  templateUrl: "./list-form.component.html",
  providers: [FaqService]
})
export class FaqListFormComponent implements OnInit {
  public faq: Faq;
  private parentId: string;
  private sub: any;
  private isEdit: boolean = false;
  private qaId: number;
  public question: string;
  public answer: string;

  public constructor(
    @Inject(FaqService) private faqService: FaqService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  public ngOnInit() {
    this.faq = new Faq(null);
    this.faq.qas = [];
    this.qaId = -1;

    this.sub = this.route.params.subscribe(params => {
      var alias = params["alias"];
      this.faqService.getByAlias(alias).subscribe(faq => this.setFaq(faq));
    });
  }

  private setFaq(faq: Faq) {
    this.faq = faq;
    this.faq.qas = [];
    var qas = JSON.parse(faq.content);
    for (var i = 0; i < qas.length; i++) {
      var qaItem = qas[i];
      var qa = new Qa();
      qa.question = qaItem.question;
      qa.answer = qaItem.answer;
      this.faq.qas.push(qa);
    }

    this.isEdit = true;
  }

  public saveQA() {
    var qa = new Qa();
    qa.question = this.question;
    qa.answer = this.answer;

    if (this.qaId === -1) {
      this.faq.qas.push(qa);
    } else {
      this.faq.qas[this.qaId] = qa;
    }

    this.faq.content = JSON.stringify(this.faq.qas);

    this.qaId = -1;
    this.question = "";
    this.answer = "";
  }

  public cancelQA() {
    this.qaId = -1;
    this.question = "";
    this.answer = "";
  }

  public edit(qaId: number) {
    this.qaId = qaId;
    var qa = this.faq.qas[qaId];
    this.question = qa.question;
    this.answer = qa.answer;
  }

  public delete(qaId: number) {
    // const modalRef = this.modalService.open(ModalComponent)
    // modalRef.componentInstance.title = "Delete Confirmation"
    // modalRef.componentInstance.body = "Delete this item?";
    // modalRef.componentInstance.button = "Delete";
    // modalRef.result.then((result) => {
    //     if (result === 'success') {
    //         this.faq.qas.splice(qaId, 1);
    //         this.faq.content = JSON.stringify(this.faq.qas);
    //     }
    // });
  }

  public save() {
    this.faq.content = JSON.stringify(this.faq.qas);
    this.faqService
      .addOrUpdate(this.faq)
      .subscribe(faq => this.saveFaqSuccess(faq));
  }

  private saveFaqSuccess(faq: Faq) {
    if (faq.id !== "0") {
      alert("Saved.");
      this.router.navigateByUrl("faq/" + faq.alias);
    } else {
      alert("Save failed.");
    }
  }

  private SaveResponse(data: any) {
    if (data !== null) {
      if (data.value !== null) {
        if (data.value === "1") {
          alert("Saved.");
          this.router.navigateByUrl("faq/" + this.faq.alias);
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
