import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import axios from "axios";
import { useSelector } from "react-redux";

// const tokenl=useSelector(state=>state.user.token);
export const apiSlice = createApi({
    reducerPath: 'api',
    baseQuery: fetchBaseQuery({baseUrl: ''}),
    tagTypes: ['FirstPhotos', 'AllPhotos', 'Posts', 'News', 'Users', 'Chats', 'Friends', 'Chat', 'Messages', 'Message', 'Comments'],
    endpoints: builder => ({
        //#region Login
        tryAuth: builder.mutation({
            queryFn: async ({email, pass}) =>{
                try{
                    const response = await axios.get('http://25.32.11.98:8085/Auth', { params: { email: email, password: pass}});
                    localStorage.setItem("token",`Bearer ${response.data}`);
                    return {data: await response.data, error: false };
                }catch(error){
                    return { error: error.message};
                }
            },
            keepUnusedDataFor: 5
        }),
        trySignUp: builder.mutation({
            queryFn: async ({fName, lName, email, pass, dateB}) =>{
                try{
                    const response = await axios.post('http://25.32.11.98:8085/Auth', { 
                        id:0,
                        firstName: fName,
                        lastName: lName,
                        email: email,
                        password: pass,
                        dateBirthday: dateB
                    });
                    localStorage.setItem("token",`Bearer ${response.data}`);
                    return {data: await response.data, error: false};
                }catch(error){
                    return { error: error.message };
                }
            }
        }),
        //#endregion
        
        //#region UserInfo
        editStatus: builder.mutation({
            queryFn: async ({id, status, token}) => {
                try{
                    const response=await axios.put("http://25.32.11.98:8086/api/User",{
                        "id":id,
                        "status":status
                      },
                    {
                      headers: {
                        'Authorization': token
                      }
                    }
                    );
                    return {data: await response.data, error: false}
                  } catch (error){
                    return { error: error.message };
                  }
            }
        }),
        editUser: builder.mutation({
            queryFn: async ({id, firstName, lastName, city, country, dataBithday, token}) => {
                try{
                    const response=await axios.put("http://25.32.11.98:8086/api/User",{
                        "id":id,
                        "first_name":firstName,
                        "last_name":lastName,
                        "city":city,
                        "country":country,
                        "date_birthday":dataBithday,
                      },
                    {
                      headers: {
                        'Authorization': token
                      }
                    }
                    );
                    return {data: await response.data, error: false}
                  } catch (error){
                    return { error: error.message };
                  }
            }
        }),
        editAvatar: builder.mutation({
            queryFn: async ({formData, token}) => {
                try{
                    const response=await axios.put("http://25.32.11.98:8086/api/Avatar",
                        formData,
                        {
                            headers:{
                                'Authorization':token,
                                'content-type': 'multipart/form-data'
                            }
                        }
                    );
                    return {data: await response.data, error: false}
                  } catch (error){
                    return { error: error.message };
                  }
            }
        }),
        editBackground: builder.mutation({
            queryFn: async ({formData, token}) => {
                try{
                    const response=await axios.put("http://25.32.11.98:8086/api/Background",
                        formData,
                        {
                            headers:{
                                'Authorization':token,
                                'content-type': 'multipart/form-data'
                            }
                        }
                    );
                    return {data: await response.data, error: false}
                  } catch (error){
                    return { error: error.message };
                  }
            }
        }),
        //#endregion
        
        //#region Photos
        getFirstPhotos: builder.query({
            query:({id, token})=>({
                url:"http://25.32.11.98:8086/api/Gallery/user",
                params:{ userId:id},
                headers:{
                    'Authorization':token
                }
                
            }),
            providesTags:['FirstPhotos'],
        }),
        getPhotos: builder.query({
            query:({id})=>({
                url:"http://25.32.11.98:8086/api/Gallery/All",
                params:{ userId:id},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
                
            }),
            providesTags:['AllPhotos']
        }),
        addPhoto: builder.mutation({
            queryFn: async({formData, token, id}) =>{
                try{
                    await axios.post("http://25.32.11.98:8086/api/Gallery",
                        formData,
                        {
                            headers:{
                                'Authorization':token,
                                'content-type': 'multipart/form-data'
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['FirstPhotos', 'AllPhotos']
        }),
        deletePhoto: builder.mutation({
            queryFn: async({id, val, token}) =>{
                try{
                    await axios.delete("http://25.32.11.98:8086/api/Gallery",
                    {
                        params:{
                            "userId":id,
                            "imageId":val
                        },
                        headers:{
                            'Authorization':token
                        }
                    }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};
                }
                
            },
            invalidatesTags:['FirstPhotos','AllPhotos']
        }),
        //#endregion
        
        //#region Posts
        getPosts: builder.query({
            query:(arg)=>({
                url:"http://25.32.11.98:8087/api/Post/All",
                params:{ userId:arg.id, number:arg.page},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
                
            }),
            providesTags: ['Posts']
        }),
        getPost: builder.query({
            query:(arg)=>({
                url:"http://25.32.11.98:8087/api/Post",
                params:{ id:arg.entity_id},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
                
            }),
            providesTags: ['Posts']
        }),
        addPost: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.post("http://25.32.11.98:8087/api/Post",
                        arg.formData,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                                'content-type': 'multipart/form-data'
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Posts', 'News']
        }),
        editPost: builder.mutation({
            queryFn: async(arg) =>{
                console.log(arg.formData);
                try{
                    await axios.put("http://25.32.11.98:8087/api/Post",
                        arg.formData,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                                'content-type': 'multipart/form-data'
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Posts']
        }),
        deletePost: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.delete("http://25.32.11.98:8087/api/Post",
                    {
                        params:{
                            userId:arg.id,
                            postId:arg.entity_id
                        },
                        headers:{
                            'Authorization':localStorage.getItem('token')
                        }
                    }
                    );
                    return{data:true, error:false};
                }catch(error){
                    return{data:false, error:error};
                }
                
            },
            invalidatesTags:['Posts']
        }),
        //#endregion
        
        //#region PostsReactions
        addReaction: builder.mutation({
            queryFn: async(data) =>{
                try{
                    await axios.post("http://25.32.11.98:8088/ReactionPost",
                        data,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Posts']
        }),
        editReaction: builder.mutation({
            queryFn: async(data) =>{
                try{
                    await axios.put("http://25.32.11.98:8088/ReactionPost",
                        data,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Posts']
        }),
        deleteReaction: builder.mutation({
            queryFn: async(data) =>{
                try{
                    await axios.delete("http://25.32.11.98:8088/ReactionPost",
                        {
                            data: data,
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};
                }
                
            },
            invalidatesTags:['Posts']
        }),
        //#endregion
        
        //#region News
        listNews: builder.query({
            query:(page=0)=>({
                url:"http://25.32.11.98:8087/api/Post/All",
                params:{number:page},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
            }),
            providesTags: ['News']
        }),
        //#endregion

        //#region Users
        listUsers: builder.query({
            query:(arg)=>({
                url:"http://25.32.11.98:8086/api/User/All",
                params:{who:arg.selection,
                        number:arg.page,
                        search: arg.search},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
            }),
            providesTags: ['Users']
        }),
        subscribeUser: builder.mutation({
            queryFn: async({data, token}) =>{
                try{
                    await axios.post("http://25.32.11.98:8086/api/Subscribe",
                        data,
                        {
                            headers:{
                                'Authorization':token,
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Users', 'Friends']
        }),
        unsubscribeUser: builder.mutation({
            queryFn: async({data, token}) =>{
                try{
                    await axios.delete("http://25.32.11.98:8086/api/Subscribe",
                        {
                            data:data,
                            headers:{
                                'Authorization':token,
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Users', 'Friends']
        }),
        getUser: builder.query({
            query:(id)=>({
                url:"http://25.32.11.98:8086/api/User",
                params:{id: id},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
            })
        }),
        getUserFirstPhotos:builder.query({
            query:(id)=>({
                url:"http://25.32.11.98:8086/api/Gallery/user",
                params:{ userId:id},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
                
            })
        }), 
        getUserPosts:builder.query({
            query:({page, id})=>({
                url:"http://25.32.11.98:8087/api/Post/All",
                params:{ userId:id, number:page},
                headers:{
                    'Authorization':localStorage.getItem('token')
                } 
            }),
        }),
        getFriends:builder.query({
            query:(arg)=>({
                url:"http://25.32.11.98:8086/api/User/All",
                params:{who:2,
                        number:arg.page,
                        search: arg.search},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
            }),
            providesTags: ['Friends']
        }),
        //#endregion

        //#region Settings
        editPrivacy: builder.mutation({
            queryFn: async(data) =>{
                console.log(data);
                try{
                    await axios.put("api/Setting/privacy",
                        data,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            }
        }),
        editNotification: builder.mutation({
            queryFn: async(data) =>{
                console.log(data);
                try{
                    await axios.put("api/Setting/notification",
                        data,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            }
        }),
        //#endregion 
    
        //#region Chats
        getChats: builder.query({
            query:(arg)=>({
                url:"http://25.32.11.98:8089/api/Chat/All",
                params:{number:arg.page, search:arg.search},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
                
            }),
            providesTags: ['Chats', 'Chat']
        }),
        getChat: builder.query({
            query:(arg)=>({
                url:"http://25.32.11.98:8089/api/Chat",
                params:{chatId:arg},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
                
            }),
            invalidatesTags: ['Chat']
        }),
        addChat:builder.mutation({
            queryFn: async(arg) =>{
                try{
                    var result = await axios.post("http://25.32.11.98:8089/api/Chat",
                        arg.data,
                        {
                            params:{
                                userId: arg.id
                            },
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    if(result.status){
                        if(result.status==401){
                            return{data:null, error:result};
                        }
                    }
                    console.log(result);
                    return{data:result.data, error:false};
                }catch(error){
                    return{data:null, error:error};     
                }
                
                // arg.Users.forEach(async (el)=>{
                //         try{
                //             let response = await axios.post("http://25.32.11.98:8089/api/Invate",
                //                 {ChatRoomId:result.data, UserId:el},
                //                 {
                                    
                //                     headers:{
                //                         'Authorization':localStorage.getItem('token'),
                //                     }
                //                 }
                //             );
                //             if(response.status){
                //                 if(response.status==401){
                //                     return{error:response};
                //                 }
                //             }
                            
                //         }catch(error){
                //             return{error:error};     
                //         }
                //     });
                // return{error:false};
            },
            invalidatesTags:['Chats']
        }),
        deleteChat: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.delete("http://25.32.11.98:8089/api/Chat",
                    {
                        params:{
                            chatId:arg.chatId
                        },
                        headers:{
                            'Authorization':localStorage.getItem('token')
                        }
                    }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};
                }
            },
            invalidatesTags:['Chats']
        }),
        inviteUser:builder.mutation({
            queryFn: async(arg) =>{
                try{
                    var result = await axios.post("http://25.32.11.98:8089/api/Invite",
                        arg,
                        {
                            
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    if(result.status){
                        if(result.status==401){
                            return{error:result};
                        }
                    }
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Chats']
        }),
        //#endregion
    
        //#region Messages
        getMessages: builder.query({
            query:(arg)=>({
                url:"http://25.32.11.98:8089/api/Message/All",
                params:{ chatId:arg.chatId, number:arg.page},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
                
            }),
            providesTags: ['Messages']
        }),
        getMessage: builder.query({
            query:(arg)=>({
                url:"http://25.32.11.98:8089/api/Message",
                params:{ messageId:arg.messageId},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
                
            }),
            
            providesTags: ['Message']
        }),
        sendMessage: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.post("http://25.32.11.98:8089/api/Message",
                        arg,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            // invalidatesTags:['Messages']
        }),
        editMessage: builder.mutation({
            queryFn: async(arg) =>{
                console.log(arg);
                try{
                    await axios.put("http://25.32.11.98:8089/api/Message",
                        arg,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            // invalidatesTags:['Messages']
        }),
        deleteMessage: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.delete("http://25.32.11.98:8089/api/Message",
                    {
                        params:{
                            messageId:arg.id
                        },
                        headers:{
                            'Authorization':localStorage.getItem('token')
                        }
                    }
                    );
                    return{data:true, error:false};
                }catch(error){
                    return{data:false, error:error};
                }
                
            },
            // invalidatesTags:['Messages']
        }),
        //#endregion
    
        //#region Reactions
        getTypeReactions: builder.query({
            query:()=>({
                url:"http://25.32.11.98:8088/TypeReaction/All",
                headers:{
                    'Authorization':localStorage.getItem('token')
                }
                
            }),
        }),
        addReactionMessage: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.post("http://25.32.11.98:8088/ReactionMessage",
                        arg,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Message']
        }),
        editReactionMessage: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.put("http://25.32.11.98:8088/ReactionMessage",
                        arg,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Message']
        }),
        deleteReactionMessage: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.delete("http://25.32.11.98:8088/ReactionMessage",
                    {
                        data: arg,
                        headers:{
                            'Authorization':localStorage.getItem('token')
                        }
                    }
                    );
                    return{data:true, error:false};
                }catch(error){
                    return{data:false, error:error};
                }
            },
            invalidatesTags:['Message']
        }),
        //#endregion

        //#region Comments
        getComments: builder.query({
            query:(arg)=>({
                url:"http://25.32.11.98:8091/api/CommentPost",
                params:{postId:arg.post_id},
                headers:{
                    'Authorization':localStorage.getItem('token')
                }                
            }),
            providesTags:['Comments']
        }),
        addComment: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.post("http://25.32.11.98:8091/api/CommentPost",
                        arg,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Comments']
        }),
        editComment: builder.mutation({
            queryFn: async(arg) =>{
                // console.log(formData);
                try{
                    await axios.put("http://25.32.11.98:8091/api/CommentPost",
                        arg,
                        {
                            headers:{
                                'Authorization':localStorage.getItem('token'),
                            }
                        }
                    );
                    return{error:false};
                }catch(error){
                    return{error:error};     
                }
            },
            invalidatesTags:['Comments']
        }),
        deleteComment: builder.mutation({
            queryFn: async(arg) =>{
                try{
                    await axios.delete("http://25.32.11.98:8091/api/CommentPost",
                    {
                        params:{
                            id:arg.id,
                        },
                        headers:{
                            'Authorization':localStorage.getItem('token')
                        }
                    }
                    );
                    return{data:true, error:false};
                }catch(error){
                    return{data:false, error:error};
                }
            },
            invalidatesTags:['Comments']
        }),
        //#endregion
    })
})
export const { useTryAuthMutation, useTrySignUpMutation, 
                useEditStatusMutation, useEditUserMutation, useEditAvatarMutation, useEditBackgroundMutation,
                useGetFirstPhotosQuery, useAddPhotoMutation, useDeletePhotoMutation, useGetPhotosQuery, useLazyGetPhotosQuery,
                useGetPostsQuery, useLazyGetPostsQuery, useAddPostMutation, useGetPostQuery, useLazyGetPostQuery, useEditPostMutation, useDeletePostMutation,
                useAddReactionMutation, useDeleteReactionMutation, useEditReactionMutation,
                useListNewsQuery, 
                useListUsersQuery, useLazyListUsersQuery, useSubscribeUserMutation, useUnsubscribeUserMutation, useGetUserQuery, useGetUserFirstPhotosQuery, useLazyGetUserPostsQuery,
                useEditPrivacyMutation, useEditNotificationMutation,
                useGetChatsQuery, useAddChatMutation, useInviteUserMutation, useDeleteChatMutation, useLazyGetChatsQuery, useGetChatQuery,
                useGetMessagesQuery, useSendMessageMutation, useEditMessageMutation, useDeleteMessageMutation, useLazyGetMessageQuery, useLazyGetMessagesQuery,
                useGetTypeReactionsQuery, useAddReactionMessageMutation, useDeleteReactionMessageMutation, useEditReactionMessageMutation,
                useGetCommentsQuery, useAddCommentMutation, useDeleteCommentMutation, useEditCommentMutation
            } = apiSlice;