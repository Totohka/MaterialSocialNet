import {
  CommentPostGetListQuery,
  CommentPostGetListReturn,
  CommentPostPostBody,
  CommentPostPutBody,
} from './comment-post-dto';
import {
  body,
  getHttpBackendClient,
  HttpBackendClient,
  queryParams,
  responseJson,
} from '@socnet/shared';

/** /api/v1/comment-post */
class CommentPostRepository {
  constructor(private _httpClient: HttpBackendClient) {}

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
  new CommentPostRepository(getHttpBackendClient());
