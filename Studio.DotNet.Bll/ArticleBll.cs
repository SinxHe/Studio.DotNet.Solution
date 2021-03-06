﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studio.DotNet.Bll
{
	// TODO 添加文章的时候, 是不是使用关联表作为参数传递(关联表中添加两个表实体的组合), 然后sql好点?
	// ReSharper disable once ClassNeverInstantiated.Global
	public class ArticleBll : BaseBll<Domain.TblArticle>, IBll.IAritlceBll
	{
		private readonly IDal.ITblArticleDal _articleDal;
		private readonly IDal.ITblTagDal _tagDal;
		private readonly IDal.ITblArticleTblTagDal _articleTagDal;
		private readonly IDal.ITblUserTblArticleDal _userArticleDal;

		public ArticleBll(
			IDal.ITblArticleDal articleDal,
			IDal.ITblTagDal tagDal,
			IDal.ITblArticleTblTagDal articleTagDal,
			IDal.ITblUserTblArticleDal userArticleDal) : base(articleDal)
		{
			_articleDal = articleDal;
			_tagDal = tagDal;
			_articleTagDal = articleTagDal;
			_userArticleDal = userArticleDal;
		}

		public async Task<int> AddAsync(Domain.TblArticle article, IEnumerable<Domain.TblTag> tags, int userId)
		{
			#region 查看标签是否已经存在

			var tblTagsList = tags.ToList();
			var tagWithIds = tblTagsList.Where(t => t.Id > 0).ToList();
			var tagWithValues = tblTagsList.Except(tagWithIds);

			var tagIdTasks = tagWithIds.Select(t => _tagDal.GetAsync(new Domain.TblTag { Id = t.Id }));    // 如果指定的tagId不存在, 报异常
			await Task.WhenAll(tagIdTasks).ConfigureAwait(false);

			var insertTagTaskList = new List<Task<int>>();
			foreach (var tag in tagWithValues)
			{
				var wait = await _tagDal.GetAsync(tag).ConfigureAwait(false);
				var temp = wait.FirstOrDefault();
				if (temp == null)
				{
					var insertTagTask = _tagDal.InsertAsync(new Domain.TblTag { Value = tag.Value });
					insertTagTaskList.Add(insertTagTask);
				}
				else
				{
					tagWithIds.Add(new Domain.TblTag { Id = temp.Id });
				}
			}
			var newTagIds = await Task.WhenAll(insertTagTaskList).ConfigureAwait(false);    // ToDO 调试的时候看看是不是生成了几个一样的id
			tagWithIds.AddRange(newTagIds.Select(id => new Domain.TblTag { Id = id }));

			#endregion
			#region 检测用户是否存在
			// TODO 检测用户是否存在
			#endregion
			// 添加文章主体
			var articleId = await _articleDal.InsertAsync(article).ConfigureAwait(false);
			// 对文章和标签进行关联
			await Task.WhenAll(tagWithIds.Select(tag => _articleTagDal.InsertAsync(new Domain.TblArticleTblTag { ArticleId = articleId, TagId = tag.Id }))).ConfigureAwait(false);
			// 对文章和用户进行关联
			await _userArticleDal.InsertAsync(new Domain.TblUserTblArticle { ArticleId = articleId, AuthorId = userId }).ConfigureAwait(false);

			return articleId;
		}

		public async Task<Domain.TblArticle> GetAsync(int id)
		{
			var result = await _articleDal.GetAsync(new Domain.TblArticle { Id = id });
			return result.Single();
		}
	}
}
