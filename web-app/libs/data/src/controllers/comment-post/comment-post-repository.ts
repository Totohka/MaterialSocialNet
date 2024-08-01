import { inject, injectable } from 'inversify';
import {
  HttpBackendClient,
  TYPES,
  DIContainer,
  body,
  queryParams,
  responseJson,
} from '@socnet/shared';
import {
  CommentPostGetListQuery,
  CommentPostGetListReturn,
  CommentPostPostBody,
  CommentPostPutBody,
} from './comment-post-dto';

/** /api/v1/comment-post */
@injectable()
export class CommentPostRepository {
  constructor(
    @inject(TYPES.HttpBackendClient) private _httpClient: HttpBackendClient
  ) {}

  /** GET /api/v1/comment-post */
  get(query: CommentPostGetListQuery) {
    return this._httpClient
      .get('/api/v1/comment-post/', queryParams(query))
      .then(responseJson<CommentPostGetListReturn>);
  }
  /** POST /api/v1/comment-post */
  post(payload: CommentPostPostBody) {
    return this._httpClient
      .post('/api/v1/comment-post/', body(payload))
      .then(responseJson<number>);
  }
  /** PUT /api/v1/comment-post */
  put(payload: CommentPostPutBody) {
    return this._httpClient
      .put('/api/v1/comment-post/', body(payload))
      .then(responseJson<void>);
  }

  /** DELETE /api/v1/comment-post/{id} */
  delete(commentId: number) {
    return this._httpClient
      .delete(`/api/v1/comment-post/${commentId}`)
      .then(responseJson<void>);
  }
}

export const getCommentPostRepository = () =>
  DIContainer.get<CommentPostRepository>(TYPES.CommentPostRepository);
