import {
  PostDeleteQuery,
  PostGetListQuery,
  PostGetListReturn,
  PostGetReturn,
  PostPostBody,
  PostPutBody,
} from './post-dto';
import {
  objectToFormData,
  body,
  HttpBackendClient,
  queryParams,
  responseJson,
  getHttpBackendClient,
} from '@socnet/shared';

/** /api/v1/post */
class PostRepository {
  constructor(private _httpClient: HttpBackendClient) {}

  /** GET /api/v1/post/{id} */
  get(idPost: number) {
    return this._httpClient
      .get(`/api/v1/post/${idPost}`)
      .then(responseJson<PostGetReturn>);
  }
  /** DELETE /api/v1/post/{id} */
  delete(idPost: number, query: PostDeleteQuery) {
    return this._httpClient
      .delete(`/api/v1/post/${idPost}`, queryParams(query))
      .then(responseJson<void>);
  }

  /** GET /api/v1/post */
  getAll(query: PostGetListQuery) {
    return this._httpClient
      .get('/api/v1/post', queryParams(query))
      .then(responseJson<PostGetListReturn>);
  }
  /** POST /api/v1/post */
  post(payload: PostPostBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .postForm('/api/v1/post', body(formData))
      .then(responseJson<number>);
  }

  /** PUT /api/v1/post */
  put(payload: PostPutBody) {
    const formData = objectToFormData(payload);
    return this._httpClient
      .putForm('/api/v1/post', body(formData))
      .then(responseJson<number>);
  }
}

export const getPostRepository = () =>
  new PostRepository(getHttpBackendClient());
