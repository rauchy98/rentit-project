import { Component, OnInit, Input } from '@angular/core';
import { CommentService, UserService } from 'app/_services';
import { Comment, User } from '../_models/index';

@Component({
	moduleId: module.id,
	selector: 'comments',
	templateUrl: './comments.component.html',
	styleUrls: [ './comments.component.css' ]
})
export class CommentsComponent implements OnInit {
	@Input() productId: number;
	comments: Comment[] = [];
	comment: Comment = new Comment();

	constructor(private commentService: CommentService, private userService: UserService) {}

	ngOnInit() {
		this.commentService.getComments(this.productId).subscribe((comments) => {
			comments.forEach((comment) => {
				this.userService.getById(comment.authorId).subscribe((user) => {
					comment.user = user;
					this.comments.unshift(comment);
				});
			});
		});
	}

	formSubmited() {
		this.commentService.addComment(this.productId, this.comment).subscribe((x) => {
			this.comment.user = new User();
			let user = JSON.parse(localStorage.getItem('user'));
			this.comments.unshift({ text: this.comment.text, id: 0, authorId: 0, user: user });
			this.comment = new Comment();
		});
	}
}
