import { Component, EventEmitter, Output } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-message-input',
  templateUrl: './message-input.component.html',
  styleUrl: './message-input.component.css'
})
export class MessageInputComponent {
  @Output() send = new EventEmitter();
  content = ''
  onSend(){
    if(this.content!.trim()){
      this.send.emit(this.content);
      
    }
    this.content='';
  }
}
